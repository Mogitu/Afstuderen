using UnityEngine;
using UnityEditor;


namespace AmcCustomPrefab
{
    /// <summary>
    /// Author  :   Maikel van Munsteren
    /// Desc    :   MVP passive style inspired GUI for the custom prefab editor. 
    /// TODO    :   Each of the presenters currently has 2 views, maybe assign each view to his own presenter to make code more readible later.
    /// </summary>
    public class PrefabManagerEditor : EditorWindow
    {        
        public string[] selStrings = new string[] { "External resources", "Build custom object" };

        private int selGridInt = 0;
        private IPrefabModel scriptModel;
        private IPresenter[] presenters;       

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
            presenters = new IPresenter[] { new ResourcesPresenter(scriptModel),
                                           new CreationPresenter(scriptModel) };           
        }

        public void OnGUI()
        {
            //Display basic options to choose what presenter to display
            GUILayout.BeginHorizontal("Box");
            selGridInt = GUILayout.SelectionGrid(selGridInt, selStrings, 2, GUILayout.Height(30));
            GUILayout.EndHorizontal();
            //Show presenter based on the selection in the bar.
            presenters[selGridInt].ShowView();            
        }
    }
}
