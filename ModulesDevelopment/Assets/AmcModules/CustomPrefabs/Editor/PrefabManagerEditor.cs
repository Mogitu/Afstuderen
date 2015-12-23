using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Reflection;
using System.IO;


namespace AmcCustomPrefab
{
    /// <summary>
    /// GUI for the custom prefab editor.
    /// TODO: Refactor to MVC/MVP/MVVM.
    /// </summary>
    public class PrefabManagerEditor : EditorWindow
    {
        private int selGridInt = 0;
        public string[] selStrings = new string[] { "External resources", "Build custom object" };
        private AmcCustomPrefab myPrefab;
        private PrefabManager man;


        private ResourcesPresenter resourcesPresenter;
        private CreationPresenter creationpresenter;

        [MenuItem("AMC Tools/AMC Object Manager")]
        static void Init()
        {
            //GetWindow will either retrieve an existing window, or create a new one if one doesn't exist.
            //PrefabManagerEditor newManager = (PrefabManagerEditor)EditorWindow.GetWindow (typeof (PrefabManagerEditor));
            PrefabManagerEditor prefabManager = (PrefabManagerEditor)EditorWindow.GetWindow(typeof(PrefabManagerEditor));
            prefabManager.titleContent = new GUIContent("AMC Prefabs");
        }

        void OnEnable()
        {
            if (man == null)
            {
                man = new PrefabManager();
            }
            // script = MonoScript.FromMonoBehaviour
            resourcesPresenter = new ResourcesPresenter();
            creationpresenter = new CreationPresenter();
        }



        public void OnGUI()
        {
            GUILayout.BeginHorizontal("Box");
            selGridInt = GUILayout.SelectionGrid(selGridInt, selStrings, 2, GUILayout.Height(30));
            GUILayout.EndHorizontal();

            switch (selGridInt)
            {
                case 0:
                    resourcesPresenter.Show();
                    break;
                case 1:
                    creationpresenter.Show();
                    break;
            }
        }
    }
}
