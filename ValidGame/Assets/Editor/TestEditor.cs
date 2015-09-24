using UnityEngine;
using UnityEditor;
using System.Collections;

public class TestEditor : EditorWindow {
 
    [MenuItem("AMC Toolkit/Card editor")]
    static void Init()
    {
        TestEditor window = EditorWindow.GetWindow<TestEditor>();
        window.Show();
    }

    void OnGUI()
    {
       
    }
}
