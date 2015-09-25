using UnityEngine;
using UnityEditor;
using System.Collections;

public class AMCMenu : EditorWindow {

    [MenuItem("AMC Tools/Card editor")]
    static void Init()
    {
        AMCMenu window = EditorWindow.GetWindow<AMCMenu>();
        window.Show();
    }


    [MenuItem("AMC Tools/spawny")]
    static void DoStuff()
    {       
            GameObject go = new GameObject("MyCreatedGO" );
            go.transform.position = new Vector3(0, 0, 0);
    }

    void OnGUI()
    {

    }
}
