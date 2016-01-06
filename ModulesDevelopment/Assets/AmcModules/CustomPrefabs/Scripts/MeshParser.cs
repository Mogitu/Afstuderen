using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AmcCustomPrefab
{
    class MeshParser : IComponentParser
    {
        public bool ParseComponent(string component, ref Lexer lex, ref GameObject go)
        {
            bool retVal = true;
            //This component will add the required components for rendering a mesh
            MeshFilter meshFilter = go.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = go.AddComponent<MeshRenderer>();
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
                    case "filename":
                        Debug.Log(lex.GetTokenType());
                        GameObject go2 = Resources.Load("card") as GameObject;
                        GameObject.Instantiate(go2);
                        // meshFilter.mesh = mesh;
                        break;
                }
              
                lex.NextToken();

            }
            return retVal;
        }
    }
}
