using UnityEngine;
using System.Collections;
using UnityEditor;


namespace AmcCustomPrefab
{
    public class ResourcesPresenter
    {
        private IView browseView;
        private IView loadResourcesView;
        private int selGridInt;
        private string[] selStrings = new string[] { "Browse", "Load ALL" };
        private PrefabManager man;
        private IPrefabModel model;
        public ResourcesPresenter(IPrefabModel model)
        {
            browseView = new BrowseView(this);
            loadResourcesView = new LoadResourcesView(this);
            man = new PrefabManager();
            this.model = model;
        }      

        public void LoadAll()
        {
            man.SpawnAllPrefabs();
        }

        public void Browse()
        {
            string data = EditorUtility.OpenFilePanel("", "", "txt");
            man.LoadSingle(data);
        }

        public void Show()
        {
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical("Box");
            selGridInt = GUILayout.SelectionGrid(selGridInt, selStrings, 1, GUILayout.Width(100));
            GUILayout.EndVertical();
            GUILayout.BeginVertical("Box");
            switch (selGridInt)
            {
                case 0:
                    browseView.Display();
                    break;
                case 1:
                    loadResourcesView.Display();
                    break;
            }
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();  
        }
    }
}

