using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AmcCustomPrefab{

	public string name;
	private string data;

	private List<string> tags = new List<string>();
	private List<string> components = new List<string>();

	private GameObject gameObjectCopy;
	
	public AmcCustomPrefab(string name, string data) {
		this.name = name;
		this.data = data;
	}

	public enum SupportedUnityComponent {
		Mesh,
		Transform,
		Collider,
        Sprite
	}

	// PrepAndVerify will parse the script to ensure it's well formatted.
	// This should be done at load time, so we can make the decision early
	// if we want to keep it. Better than returning error half way into the
	// game when we try to load the prefab.
	public bool PrepAndVerify() {
		return Parse();
	}

	public bool HasComponent(Type type) {
		string typeName = type.Name;
		return components.Contains(typeName);
	}

	private bool Parse() {
		//Due to the limitations of Unity (can't create GameObjects without also instantiating them)
		// we're going to temporarily create a game object, get some data from it, and then destroy it
		//This will verify the syntax of the file
		gameObjectCopy = new GameObject(name);

		tags.Clear();
		components.Clear();

		bool retVal = Parse(ref gameObjectCopy);

		gameObjectCopy.SetActive (false);
		gameObjectCopy.hideFlags = HideFlags.HideAndDontSave;

		if(!retVal) {
			Debug.Log("Error parsing!");
			if(Application.isEditor) {
				UnityEngine.Object.DestroyImmediate(gameObjectCopy);
			} else {
				GameObject.Destroy(gameObjectCopy);
			}
		}
		return retVal;
	}

	public bool Parse(ref GameObject go) {
		bool retVal = true;
		Lexer lex = new Lexer(data);
		lex.NextToken();
		while(lex.GetTokenType() != Lexer.TokenType.EndOfInput) {
			switch(lex.GetTokenType()) {
			case Lexer.TokenType.ComponentToken:
				string componentName = lex.GetToken();
				
				//ensure correct syntax to continue
				lex.NextToken();
				if(lex.Match("]")) {
					lex.NextToken();
					if(lex.Match("{")) {
						lex.NextToken();
					} else {
						Debug.Log("Component declaration must be followed by an open bracket`{`");
						lex.Dispose();
						return false;
					}
				} else {
					Debug.Log("Component name must not have any spaces and must be closed with a square bracket `]`");
					lex.Dispose();
					return false;
				}
				//now continue on to parse component body

				if(Enum.IsDefined(typeof(SupportedUnityComponent), componentName)) {
					//if the component is named as one of the built in supported unity components, use a special parser.
					retVal &= ParseUnityComponent((SupportedUnityComponent)Enum.Parse(typeof(SupportedUnityComponent), componentName), ref lex, ref go);
				} else {
					//otherwise use the parser for our custom components.
					retVal &= ParseCustomComponent(componentName, ref lex, ref go);
				}

				break;
			case Lexer.TokenType.IdentifierToken:
				if(lex.Match("Tags")) {
					lex.NextToken();
					if(lex.Match("{")) {
						lex.NextToken();
						ParseTags(ref lex);
					}
				} else {
					Debug.Log("Unexpected token: `" + lex.GetToken() + "`. Expected `Tags` token at this level.");
				}
				break;
			default: //other tokens we might care about at this level would be meta information for the entity, like name, type and so on. Information specific to an entity.
				Debug.Log("Unexpected token: " + lex.GetToken());
				break;
			}
			lex.NextToken();
		}
		lex.Dispose();
		return retVal;
	}

	private bool ParseUnityComponent(SupportedUnityComponent component, ref Lexer lex, ref GameObject go) {
		bool retVal = true;
		switch(component) {
		case SupportedUnityComponent.Transform:
			while(!lex.Match("}") && lex.GetTokenType() != Lexer.TokenType.EndOfInput) {
				string field = lex.GetToken();
				lex.NextToken();//equals symbol
				if(lex.Match("=")) {
					lex.NextToken();
				} else {
					Debug.Log("Syntax Error: Expected `=` after field name");
					lex.NextToken();//try to continue anyway?
				}
				switch(field.ToLower()) {
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

		case SupportedUnityComponent.Mesh:
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

		case SupportedUnityComponent.Collider:
			Debug.Log(component);
			//TODO Add support for defining our own colliders
			//This component would accept a shape and dimensions            
			while(!lex.Match("}") && lex.GetTokenType() != Lexer.TokenType.EndOfInput) {
				lex.NextToken();
			}
			retVal = false;
			break;
        case SupportedUnityComponent.Sprite:
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
                        ColorUtility.TryParseHtmlString(((string)color).ToLower(),out col);
                        spr.color = col;
                        break;                  
                }
                lex.NextToken();
            }
			break;
		default:
			//looks like we added a keyword, but not the parsing code?
			Debug.Log("Don't know how to parse Unity component: " + component);
			retVal = false;
			break;
		}
		return retVal;
	}

	private bool ParseCustomComponent(string componentName, ref Lexer lex, ref GameObject go) {
		//AmcComponent customComponent = UnityEngineInternal.APIUpdaterRuntimeServices.AddComponent(go, "Assets/Scripts/Tools/AdvCustomPrefab.cs (223,34)", componentName) as AmcComponent;
        AmcComponent customComponent = go.AddComponent(System.Type.GetType(componentName)) as AmcComponent;
        bool setDataSuccess = false;
		if(customComponent != null) {
			setDataSuccess = customComponent.SetData(ref lex);
		} else {
			Debug.Log("Component " + componentName + " couldn't be added! Ensure it exists or isn't being added twice.");
		}
		return setDataSuccess;
	}
	
	public GameObject Instantiate() {
		if(gameObjectCopy != null) {
			GameObject go = GameObject.Instantiate(gameObjectCopy) as GameObject;
			go.SetActive (true);
			go.hideFlags = HideFlags.None;
			return go;
		} else {
			return null;
		}
	}
	
	private void ParseTags(ref Lexer lex) {
		while(!lex.Match("}") && lex.GetTokenType() != Lexer.TokenType.EndOfInput) {
			if(lex.GetTokenType() == Lexer.TokenType.IdentifierToken) {
				tags.Add(lex.GetToken());
			}
			lex.NextToken();
		}
	}
	
	public Mesh GetMesh() {
		if(gameObjectCopy != null && gameObjectCopy.GetComponent<MeshFilter>() != null)
			return gameObjectCopy.GetComponent<MeshFilter>().mesh;
		return null;
	}

	public Vector3 GetScale() {
		if(gameObjectCopy != null)
			return gameObjectCopy.transform.localScale;
		return Vector3.one;
	}

	public List<string> GetTags() {
		return tags;
	}

}

