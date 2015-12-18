using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class AmcPrefabLoader : MonoBehaviour {

	Dictionary<string, AmcCustomPrefab> prefabs = new Dictionary<string, AmcCustomPrefab>();
	
	// Use this for initialization
	void Start () {
		//A statically loaded folder for this demo
		LoadPrefabFolder(@"Assets\");
	}

	public void LoadPrefabFolder(string dataFolder) {
		//get all the files ending with .data in the specified directory
		foreach(string dataFile in Directory.GetFiles(dataFolder, "*.advdata", SearchOption.AllDirectories)) {
			string fileContents = File.ReadAllText(dataFile);
			string name = dataFile.Substring(dataFile.LastIndexOf("\\")+1, dataFile.LastIndexOf(".") - (dataFile.LastIndexOf("\\")+1));
			AmcCustomPrefab prefab = new AmcCustomPrefab(name, fileContents);
			if(prefab.PrepAndVerify())
				prefabs.Add(name, prefab);
		}
	}

	public GameObject InstantiatePrefab(string prefabName) {
		if(prefabs.ContainsKey(prefabName)) {
			return prefabs[prefabName].Instantiate();
		} else {
			Debug.Log("Prefab not loaded");
		}
		return null;
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.A)) {
			InstantiatePrefab("DataExample1");
		}
		if(Input.GetKeyDown(KeyCode.B)) {
			InstantiatePrefab("TestEntityB");
		}
	}

}
