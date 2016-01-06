using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;


namespace AmcCustomPrefab
{
    /// <summary>
    /// Author  :   Maikel van Munsteren
    /// Desc    :   Presenter for the resources
    /// </summary>
    public class ResourcesPresenter : IPresenter
    {
        private IView[] views;
        private int selGridInt;
        private string[] selStrings; 
        private IPrefabModel model;

        public ResourcesPresenter(IPrefabModel model)
        {
            selStrings = new string[] { "Browse",
                                        "Load ALL" };
            views = new IView[] { new BrowseView(this) ,
                                  new LoadResourcesView(this) };
            this.model = model;
        }

        public void LoadAll()
        {
            LoadAllCustomprefabs();
        }

        public void BrowseAndLoad(string path)
        {
            LoadSingle(path);
        }

        public void ShowView()
        {
            //Format the layout for the data
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical("Box");
            selGridInt = GUILayout.SelectionGrid(selGridInt, selStrings, 1, GUILayout.Width(100));
            GUILayout.EndVertical();
            GUILayout.BeginVertical("Box");
            //display the 
            views[selGridInt].Display();
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }


        /// <summary>
        /// Loads all prefabs that are available in the Resources folder.
        /// </summary>
        private void LoadAllCustomprefabs()
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

                //Create a new prefab using the data from the file
                AmcCustomPrefab myPrefab = new AmcCustomPrefab(name, fileContents);
                //Prep and verify. this means parse the data to ensure it's accurate and get
                //some information from it, like the mesh and scale, for potential future use.
                if (myPrefab.PrepAndVerify())
                {
                    //Place the object in the scene
                    myPrefab.Instantiate();
                }
                else
                {
                    Debug.Log("Error: Prefab `" + name + "` had parse errors and cannot be loaded.");
                }
            }
        }

        /// <summary>
        /// Loads a single selected prefab from a text file.
        /// </summary>
        /// <param name="path">the path to the file.</param>
        private void LoadSingle(string path)
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
                myPrefab.Instantiate();
            }
            else
            {
                Debug.Log("Error: Prefab `" + name + "` had parse errors and cannot be loaded.");
            }
        }
    }
}

