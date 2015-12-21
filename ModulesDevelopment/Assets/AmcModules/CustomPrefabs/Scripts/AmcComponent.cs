using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Text;

namespace AmcCustomPrefab
{       
    public class AmcComponent : MonoBehaviour, IScriptGenerator, IDataSetter
    {
        public virtual bool SetData(ref Lexer lex)
        {
            bool returnValue = true;
            List<string> valuesSet = new List<string>(); //an easy way to keep track of the values we've already set

            while (!lex.Match("}") && lex.GetTokenType() != Lexer.TokenType.EndOfInput)
            {
                //for each token until the end of the component definition or the end of input
                FieldInfo fieldInfo = this.GetType().GetField(lex.GetToken());
                if (fieldInfo != null)
                {
                    System.Object value = lex.GetValue();

                    //Handle our special cases. These are types not defined in Unity, but defined in our scripting language
                    // for example, here we're going to select values from the range type values defined in our scripts.
                    Lexer.FinializeSpecialTypes(ref value, lex.GetTokenType());

                    if (fieldInfo.FieldType.IsEnum)
                    {
                        fieldInfo.SetValue(this, Enum.Parse(fieldInfo.FieldType, value.ToString()));
                    }
                    else
                    {
                        fieldInfo.SetValue(this, value);
                    }

                    valuesSet.Add(fieldInfo.Name);
                }
                else
                { //if the current token hasn't been set, it's something we don't know what to do with
                    Debug.Log("Warning: Unknown property: `" + lex.GetToken() + "` on component `" + this.GetType().Name + "`");
                    lex.GetValue(); //get the value to clear it from the queue
                }
                lex.NextToken(); //get the next token to prepare for the next iteration
            }
            //now that we've parsed all the tokens, check over our fields to make sure we didn't miss any that are required to be set
            foreach (FieldInfo fieldInfo in this.GetType().GetFields(BindingFlags.Public |
                                                                    BindingFlags.Instance |
                                                                    BindingFlags.DeclaredOnly))
            {
                if (RequiresDefinition(fieldInfo) && !valuesSet.Contains(fieldInfo.Name))
                {
                    Debug.Log("Error: The property: `" + fieldInfo.Name + "`, is set `RequiresDefinition` and is not defined!");
                    returnValue = false;
                }
            }
            return returnValue;
        }

        public class PrefabAttribute : Attribute
        {
            public bool RequiresDefinition { get; set; }
        }

        public static bool RequiresDefinition(FieldInfo fieldInfo)
        {
            foreach (System.Object customAttribute in fieldInfo.GetCustomAttributes(true))
            {
                if (customAttribute.GetType() == typeof(PrefabAttribute))
                {
                    return ((PrefabAttribute)customAttribute).RequiresDefinition;
                }
            }
            return false;
        }


        //Added for Custom Editor
        public string GetPropertyString(FieldInfo fieldInfo)
        {
            if (fieldInfo.FieldType == typeof(string))
                return "\"" + fieldInfo.GetValue(this).ToString() + "\"";
            else if (fieldInfo.FieldType == typeof(Vector3))
                return fieldInfo.GetValue(this).ToString().Replace("(", "").Replace(")", "");
            else
                return fieldInfo.GetValue(this).ToString();
        }


        //Added for Custom Editor
        public virtual string GenerateComponentScript()
        {
            StringBuilder generatedScript = new StringBuilder();
            generatedScript.AppendLine("[" + this.GetType().Name + "]{");

            foreach (FieldInfo fieldInfo in this.GetType().GetFields(BindingFlags.Public |
                                                                    BindingFlags.Instance |
                                                                    BindingFlags.DeclaredOnly))
            {

                generatedScript.AppendLine(fieldInfo.Name + "=" + GetPropertyString(fieldInfo));

            }
            generatedScript.AppendLine("}");
            return generatedScript.ToString();
        }
    }


}
