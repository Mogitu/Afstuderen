using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

namespace AmcCustomPrefab
{
    /// <summary>
    /// Used to inherit from monobehaviour! DO NOT USE!
    /// </summary>
    public class PrefabManager
    {
        private List<string> allPrefabs = new List<string>();
        private Dictionary<string, AmcCustomPrefab> customPrefabs = new Dictionary<string, AmcCustomPrefab>();
        private Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();
        private Dictionary<string, List<string>> prefabsByTag = new Dictionary<string, List<string>>();

        public void SpawnAllPrefabs()
        {
            LoadAllCustomPrefabs();
            foreach (KeyValuePair<string, AmcCustomPrefab> prefab in customPrefabs)
            {
                this.Instantiate(prefab.Key);
            }
        }

        public void LoadSingle(string path)
        {
            StreamReader reader = new StreamReader(path);
            string name = "yep";
            //Read all the data from the file
            string fileContents = reader.ReadToEnd();
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
                this.Instantiate(name);
            }
            else
            {
                Debug.Log("Error: Prefab `" + name + "` had parse errors and cannot be loaded.");
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
                if (!dataFile.name.ToLower().Contains("entity"))
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
    }
}
