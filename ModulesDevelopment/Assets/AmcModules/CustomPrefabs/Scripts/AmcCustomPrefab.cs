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
                    IComponentParser parser;
				if(Enum.IsDefined(typeof(SupportedUnityComponent), componentName)) {
                        parser = (IComponentParser)Activator.CreateInstance(Type.GetType(componentName + "Parser"));
                        //new UnityComponentParser();
                        //if the component is named as one of the built in supported unity components, use a special parser.					
                    } else {
                        parser = new CustomComponentParser();
                        //otherwise use the parser for our custom components.                      
				}
                    retVal &=parser.ParseComponent(componentName, ref lex, ref go);
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

