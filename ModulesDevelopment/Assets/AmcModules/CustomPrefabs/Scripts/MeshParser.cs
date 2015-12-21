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

                //Ideally this would be expanded to also search through all loaded custom meshes (not use a switch statement)
                //However, for this tutorial, we'll take a shortcut and only allow these primitives
                if (lex.Match("meshtype"))
                {
                    string meshType = (string)lex.GetValue(Lexer.TokenType.IdentifierToken);
                    switch (meshType.ToLower())
                    {
                        case "cube":
                            //meshFilter.mesh = GameObject.FindObjectOfType<PrefabManager>().primitiveCube;
                            break;
                        case "sphere":
                            //meshFilter.mesh = GameObject.FindObjectOfType<PrefabManager>().primitiveSphere;
                            break;
                        case "capsule":
                            //	meshFilter.mesh = GameObject.FindObjectOfType<PrefabManager>().primitiveCapsule;
                            break;
                        default:
                            Debug.Log("Mesh type: `" + meshType + "` not supported!");
                            retVal = false;
                            break;
                    }
                }
                if (lex.Match("material"))
                {
                    string material = (string)lex.GetValue(Lexer.TokenType.IdentifierToken);
                    switch (material.ToLower())
                    {
                        case "default":
                            //	meshRenderer.material = GameObject.FindObjectOfType<PrefabManager>().defaultMaterial;
                            break;
                        default:
                            Debug.Log("Material: `" + material + "` not supported!");
                            retVal = false;
                            break;
                    }
                }

                lex.NextToken();

            }
            return retVal;
        }
    }
}
