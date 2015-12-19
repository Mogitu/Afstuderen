using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class UnityComponentParser : IComponentParser
{
    public bool ParseComponent(string component, ref Lexer lex, ref GameObject go)
    {
        bool retVal = true;
        switch (component)
        {
            /*
            case "Transform":
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
                break;
            */
            case "Mesh":
                /*
			//This component will add the required components for rendering a mesh
			MeshFilter meshFilter = go.AddComponent<MeshFilter>();
			MeshRenderer meshRenderer = go.AddComponent<MeshRenderer>();
			while(!lex.Match("}") && lex.GetTokenType() != Lexer.TokenType.EndOfInput) {

				//Ideally this would be expanded to also search through all loaded custom meshes (not use a switch statement)
				//However, for this tutorial, we'll take a shortcut and only allow these primitives
				if(lex.Match("meshtype")) {
					string meshType = (string)lex.GetValue(Lexer.TokenType.IdentifierToken);
					switch(meshType.ToLower()) {
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
				if(lex.Match("material")) {
					string material = (string)lex.GetValue(Lexer.TokenType.IdentifierToken);
					switch(material.ToLower()) {
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
                 */
                break;

            case "Collider":
                Debug.Log(component);
                //TODO Add support for defining our own colliders
                //This component would accept a shape and dimensions            
                while (!lex.Match("}") && lex.GetTokenType() != Lexer.TokenType.EndOfInput)
                {
                    lex.NextToken();
                }
                retVal = false;
                break;
                /*
            case "Sprite":
                SpriteRenderer spr = go.AddComponent<SpriteRenderer>();
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
                        case "color":
                            System.Object color = lex.GetObject();
                            Lexer.FinializeSpecialTypes(ref color, lex.GetTokenType());
                            Color col;
                            ColorUtility.TryParseHtmlString(((string)color).ToLower(), out col);
                            spr.color = col;
                            break;
                    }
                    lex.NextToken();
                }
                break;
                */
            default:
                //looks like we added a keyword, but not the parsing code?
                Debug.Log("Don't know how to parse Unity component: " + component);
                retVal = false;
                break;
        }
        return retVal;
    }
}

