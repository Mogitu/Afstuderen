using UnityEngine;
using UnityEditor;


namespace AmcCustomPrefab
{
    /// <summary>
    /// MVP passive style inspired GUI for the custom prefab editor. 
    /// </summary>
    public class PrefabManagerEditor : EditorWindow
    {        
        public string[] selStrings = new string[] { "External resources", "Build custom object" };

        private int selGridInt = 0;
        private IPrefabModel scriptModel;
        private ResourcesPresenter resourcesPresenter;
        private CreationPresenter creationpresenter;

        [MenuItem("AMC Tools/AMC Object Manager")]
        static void Init()
        {            
            PrefabManagerEditor prefabManager = (PrefabManagerEditor)EditorWindow.GetWindow(typeof(PrefabManagerEditor));
            prefabManager.titleContent = new GUIContent("AMC Prefabs");
        }

        //Called after opening the window
        void OnEnable()
        {        
            scriptModel = new PrefabModel();      
            resourcesPresenter = new ResourcesPresenter(scriptModel);
            creationpresenter = new CreationPresenter(scriptModel);
        }

        public void OnGUI()
        {
            //Display basic options to choose what presenter to display
            GUILayout.BeginHorizontal("Box");
            selGridInt = GUILayout.SelectionGrid(selGridInt, selStrings, 2, GUILayout.Height(30));
            GUILayout.EndHorizontal();

            //Show presenter based on the selection in the bar.
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
