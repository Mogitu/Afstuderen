using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Reflection;
using System.IO;
using System.Collections.Generic;

namespace AmcCustomPrefab
{
    /// <summary>
    /// Author  :   Maikel van Munsteren.
    /// Desc    :   Presenter for the script and select views.
    /// </summary>
    public class CreationPresenter : IPresenter
    {
        private IView[] views;     
        private int selGridInt;
        private string[] selStrings = new string[] { "Select it!", "Script it!" };
        public IPrefabModel model;

        //script vars     
        private bool validScript = false;
        private bool error = false;

        //Selection vars
        private ScriptableObject scrObj;
        private SerializedObject obj;

        public CreationPresenter(IPrefabModel model)
        {
            this.model = model;
            views = new IView[]{ new SelectView(this),
                                 new ScriptView(this)};
        }

        public void ShowView()
        {
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical("Box");
            selGridInt = GUILayout.SelectionGrid(selGridInt, selStrings, 1, GUILayout.Width(100));
            GUILayout.EndVertical();
            GUILayout.BeginVertical("Box");
            views[selGridInt].Display();
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }

        #region scripting functions
        public void SetScriptToParse(string script)
        {
            model.DataString = script;
        }

        public void ParseScript()
        {
            //Pass in the contents of the textarea as the script for this prefab
            model.CustomPrefab = new AmcCustomPrefab("TestPrefab", model.DataString);
            if (model.CustomPrefab.PrepAndVerify())
            {
                validScript = true;
                error = false;
            }
            else
            {
                validScript = false;
                error = true;
            }
        }     

        public void InvalidateScript()
        {
            //invalidate our prefab and script when the script changes
            validScript = false;
            error = false;
        }

        public void InstantiatePrefab()
        {
            model.CustomPrefab.Instantiate();
        }

        public void SavePrefab(string path)
        {
            string savePath = path;
            System.IO.File.WriteAllText(savePath, model.DataString);
        }

        public string ScriptToParse
        {
            get { return model.DataString; }
        }

        public bool ValidScript
        {
            get  { return validScript;   }            
        }

        public bool Error
        {
            get { return error; }
        }
        #endregion
        #region selection functions
        public void SetScript(MonoScript script)
        {
            model.Script = script;
        }

        public MonoScript GetScript()
        {
            return model.Script;
        }

        public void WorkOnScript()
        {
            model.RequiredProperties = new List<SerializedProperty>();
            model.OptionalProperties = new List<SerializedProperty>();
            if (model.Script != null)
            {
                model.TmpGo = new GameObject();
                model.TmpGo.hideFlags = HideFlags.HideAndDontSave;
                model.TmpComp = model.TmpGo.AddComponent(model.Script.GetClass()) as AmcComponent;


                scrObj = ScriptableObject.CreateInstance(model.Script.GetClass());
                obj = new SerializedObject(scrObj);
                foreach (FieldInfo fieldInfo in model.TmpComp.GetType().GetFields(BindingFlags.Public |
                                                                              BindingFlags.Instance |
                                                                              BindingFlags.DeclaredOnly))
                {

                    if (AmcComponent.RequiresDefinition(fieldInfo))
                    {

                        SerializedProperty requiredProperty = obj.FindProperty(fieldInfo.Name);

                        if (requiredProperty != null)
                        {
                            model.RequiredProperties.Add(requiredProperty);
                        }
                    }
                    else {

                        SerializedProperty optionalProperty = obj.FindProperty(fieldInfo.Name);
                        if (optionalProperty != null)
                        {
                            model.OptionalProperties.Add(optionalProperty);
                        }
                    }
                }
                obj.Update();
            }
        }

        public void SaveSelectedModel(string destination)
        {
            GameObject newGo = GameObject.Instantiate(model.TmpGo);
            AmcComponent[] comps = newGo.GetComponents<AmcComponent>();
            foreach (AmcComponent cmp in comps)
            {
               
                File.WriteAllText(destination, cmp.GenerateComponentScript());
            }
        }
        #endregion
    }
}