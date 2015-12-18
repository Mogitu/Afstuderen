using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Used to inherit from monobehaviour! DO NOT USE!
/// </summary>
public class PrefabManager
{
    public bool displayPrefabMenu = true;
    public bool showAll = false;

    public List<string> allPrefabs = new List<string>();

    private Dictionary<string, AmcCustomPrefab> customPrefabs = new Dictionary<string, AmcCustomPrefab>();
    Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();
    Dictionary<string, List<string>> prefabsByTag = new Dictionary<string, List<string>>();

    GameObject pickedPrefab;
    // Use this for initialization
    void Start()
    {
        //Load the custom prefabs first. We want any custom prefabs to supercede Unity prefabs.
        //This allows you, or users, to override existing prefabs with custom ones.
        //If we don't want to allow that, simply reverse the load order.
        //LoadAllCustomPrefabs();
        //LoadAllUnityPrefabs();       
        //Debug.Log("Prefabs loaded!");
    }

    public void SpawnAllPrefabs()
    {
        LoadAllCustomPrefabs();
        foreach (KeyValuePair<string, AmcCustomPrefab> prefab in customPrefabs)
        {
            this.Instantiate(prefab.Key);
        }
    }

    private void LoadAllCustomPrefabs()
    {
        //Get all the txt files in the resources directory
        //If we wanted to allow users to add their own scripts, we would instead
        // copy these files externally on install and load them from there
        foreach (TextAsset dataFile in Resources.LoadAll<TextAsset>(""))
        {
            //This is simply to keep other files in this project from loading
            // for a cleaner demonstation
            if (!dataFile.name.ToLower().Contains("entity") && !showAll)
                continue;

            //Read all the data from the file
            string fileContents = dataFile.text;
            //Get the name of the prefab, as the file name
            string name = dataFile.name;
            //If we don't already have a prefab by that name, go about adding it
            if (!allPrefabs.Contains(name))
            {
                //Create a new prefab using the data from the file
                AmcCustomPrefab myPrefab = new AmcCustomPrefab(name, fileContents);
                //Prep and verify. this means parse the data to ensure it's accurate and get
                //some information from it, like the mesh and scale, for potential future use
                if (myPrefab.PrepAndVerify())
                {
                    //If it's passed the verify, add it to our list
                    AddCustomPrefab(myPrefab, name);

                    //Add a reference to for each of the tags defined in the data file
                    //This allows us to group prefabs, so we could, for example,
                    // get all the prefabs tagged enemy and spawn one randomly
                    foreach (string tag in customPrefabs[name].GetTags())
                    {
                        if (!prefabsByTag.ContainsKey(tag))
                        {
                            prefabsByTag.Add(tag, new List<string>());
                        }
                        prefabsByTag[tag].Add(name);
                    }
                }
                else
                {
                    Debug.Log("Error: Prefab `" + name + "` had parse errors and cannot be loaded.");
                }
            }
            else
            {
                //Debug.Log("Error: Duplicate prefab defined! `" + name + "`");
            }
        }
    }

    private void AddCustomPrefab(AmcCustomPrefab prefab, string name)
    {
        customPrefabs.Add(name, prefab);
        allPrefabs.Add(name);
    }

    /*
	private void LoadAllUnityPrefabs() {
		//Utilize the Resources class to automatically located the "Resources" directory
		//Load each object that's a GameObject, that means prefabs.
		foreach(GameObject go in Resources.LoadAll("", typeof(GameObject))) {

			//This is simply to keep other files in this project from loading
			// for a cleaner demonstation
			if(!go.name.ToLower().Contains("entity") && !showAll)
				continue;


			//Give ourselves a way to ignore some prefabs, like using the leading '_'
			if(go.name.StartsWith("_") || prefabs.ContainsKey(go.name))
				continue;

			if(!allPrefabs.Contains(go.name)) {
				prefabs.Add(go.name, go);
				allPrefabs.Add(go.name);

				//Unity prefabs only allow one tag.
				if(!prefabsByTag.ContainsKey(go.tag)) {
					prefabsByTag.Add(go.tag, new List<string>());
				}

				prefabsByTag[go.tag].Add(go.name);
			} else {
				//Debug.Log("Error: Duplicate prefab defined! `" + name + "`");
			}
		}
	}
     * */

    public Mesh GetMeshFor(string prefabName)
    {
        if (customPrefabs.ContainsKey(prefabName))
        {
            return customPrefabs[prefabName].GetMesh();
        }
        if (prefabs.ContainsKey(prefabName))
        {
            MeshFilter meshFilter = prefabs[prefabName].GetComponent<MeshFilter>();
            if (meshFilter != null)
            {
                return meshFilter.sharedMesh;
            }
            else
            {
                return null;
            }
        }
        return null;
    }

    public Vector3 GetScaleFor(string prefabName)
    {
        if (customPrefabs.ContainsKey(prefabName))
        {
            return customPrefabs[prefabName].GetScale();
        }
        if (prefabs.ContainsKey(prefabName))
        {
            return prefabs[prefabName].transform.localScale;
        }
        return new Vector3(1, 1, 1);
    }

    public GameObject Instantiate(string prefabName)
    {
        if (customPrefabs.ContainsKey(prefabName))
        {
            return customPrefabs[prefabName].Instantiate();
        }
        if (prefabs.ContainsKey(prefabName))
        {
            return GameObject.Instantiate(prefabs[prefabName]) as GameObject;
        }
        return null;
    }

    public GameObject Instantiate(string prefabName, Vector3 position, Quaternion rotation)
    {
        if (customPrefabs.ContainsKey(prefabName))
        {
            GameObject droppedPrefab = customPrefabs[prefabName].Instantiate();
            droppedPrefab.transform.position.Set(position.x, position.y, position.z);
            droppedPrefab.transform.rotation.Set(rotation.x, rotation.y, rotation.z, rotation.w);
            droppedPrefab.name = droppedPrefab.name.Replace("(Clone)", "");
            return droppedPrefab;
        }
        if (prefabs.ContainsKey(prefabName))
        {
            GameObject droppedPrefab = GameObject.Instantiate(prefabs[prefabName], position, rotation) as GameObject;
            droppedPrefab.name = prefabName.Replace("(Clone)", ""); ;
            return droppedPrefab;
        }
        return null;
    }

    List<string> GetPrefabsWithTag(string tag)
    {
        if (prefabsByTag.ContainsKey(tag))
        {
            return prefabsByTag[tag];
        }
        else
        {
            return null;
        }
    }
}
