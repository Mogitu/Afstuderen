using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AmcCustomPrefab
{
    class TransformParser : IComponentParser
    {
        public bool ParseComponent(string component, ref Lexer lex, ref GameObject go)
        {
            bool retVal = true;
            while (!lex.Match("}") && lex.GetTokenType() != Lexer.TokenType.EndOfInput)
            {
                string field = lex.GetToken();
                lex.NextToken();//equals symbol
                if (lex.Match("="))
                {
                    lex.NextToken();
                }
                else
                {
                    Debug.Log("Syntax Error: Expected `=` after field name");
                    lex.NextToken();//try to continue anyway?
                }
                switch (field.ToLower())
                {
                    case "position":
                        System.Object position = lex.GetObject();
                        Lexer.FinializeSpecialTypes(ref position, lex.GetTokenType());
                        go.transform.position = (Vector3)position;
                        break;
                    case "rotation":
                        System.Object rotation = lex.GetObject();
                        Lexer.FinializeSpecialTypes(ref rotation, lex.GetTokenType());
                        go.transform.rotation = Quaternion.Euler((Vector3)rotation);
                        break;
                    case "scale":
                        System.Object scale = lex.GetObject();
                        Lexer.FinializeSpecialTypes(ref scale, lex.GetTokenType());
                        go.transform.localScale = (Vector3)scale;
                        break;
                    default:
                        Debug.Log("`" + lex.GetToken() + "` not a supported field of Transform");
                        retVal = false;
                        break;
                }
                lex.NextToken();
            }
            return retVal;
        }
    }


}
