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
        private static string scriptToParse = "";
        private bool validScript = false;
        private bool error = false;

        private int selGridInt = 0;
        public string[] selStrings = new string[] { "External resources", "Build custom object" };
        public string[] buildStrings = new string[] { "Script it!", "Select it!" };
        private int selBuildInt = 0;
        private AmcCustomPrefab myPrefab;
        private PrefabManager man;

        private MonoScript script;
        private List<SerializedProperty> requiredProperties;
        private List<SerializedProperty> optionalProperties;
        private AmcComponent tmpComp;
        private GameObject tmpGo;

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
        }

        void SpawnAllPrefabs()
        {

        }

        public void OnGUI()
        {
            GUILayout.BeginHorizontal("Box");
            selGridInt = GUILayout.SelectionGrid(selGridInt, selStrings, 2, GUILayout.Height(30));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (selGridInt == 0)
            {
                if(GUILayout.Button("Load"))
                {
                    string data = EditorUtility.OpenFilePanel("","","txt");
                    man.LoadSingle(data);
                }

                if (GUILayout.Button("Load Resource folder"))
                {
                    man.SpawnAllPrefabs();
                }
            }
            else if (selGridInt == 1)
            {
                GUILayout.BeginVertical("Box");
                selBuildInt = GUILayout.SelectionGrid(selBuildInt, buildStrings, 1, GUILayout.Width(100), GUILayout.Height(100));
                GUILayout.EndVertical();
                GUIA();
                GUIB();
            }
            GUILayout.EndHorizontal();
        }


        private void GUIA()
        {
            GUILayout.BeginVertical("Box");
            if (selBuildInt == 0)
            {
                bool wrap = EditorStyles.textField.wordWrap;
                EditorStyles.textField.wordWrap = true;
                //Create a text area that fills the window
                string updatedScript = EditorGUILayout.TextArea(scriptToParse, GUILayout.ExpandHeight(true));
                EditorStyles.textField.wordWrap = wrap;

                if (!updatedScript.Equals(scriptToParse))
                {
                    //invalidate our prefab and script when the script changes
                    validScript = false;
                    error = false;
                }
                scriptToParse = updatedScript;

                if (GUILayout.Button("Parse script"))
                {
                    //Pass in the contents of the textarea as the script for this prefab
                    myPrefab = new AmcCustomPrefab("TestPrefab", scriptToParse);
                    if (myPrefab.PrepAndVerify())
                    {
                        validScript = true;
                        error = false;
                    }
                    else
                    {
                        validScript = false;
                        error = true;
                    }
                }

                //If it's valid, let the user know in the window, and give them the option to instantiate a copy.
                if (validScript)
                {
                    EditorGUILayout.HelpBox("Parse complete. No errors, check console for any warnings.", MessageType.Info, true);
                    if (GUILayout.Button("Instantiate"))
                    {
                        myPrefab.Instantiate();
                    }
                    if (GUILayout.Button("Save"))
                    {
                        string savePath = EditorUtility.SaveFilePanel("t", "", "", "txt");
                        System.IO.File.WriteAllText(savePath, scriptToParse);
                    }
                }

                //If there's an error, the user know.
                if (error)
                {
                    EditorGUILayout.HelpBox("Error parsing! See console for errors.", MessageType.Error, true);
                }
            }
        }

        private void GUIB()
        {
            if (selBuildInt == 1)
            {
                script = EditorGUILayout.ObjectField("Script", script, typeof(MonoScript), false) as MonoScript;
                ScriptableObject scrObj;
                SerializedObject obj;
                if (script != null)
                {
                    tmpGo = new GameObject();
                    tmpGo.hideFlags = HideFlags.HideAndDontSave;
                    tmpComp = tmpGo.AddComponent(script.GetClass()) as AmcComponent;

                    requiredProperties = new List<SerializedProperty>();
                    optionalProperties = new List<SerializedProperty>();
                    scrObj = ScriptableObject.CreateInstance(script.GetClass());
                    obj = new SerializedObject(scrObj);
                    foreach (FieldInfo fieldInfo in tmpComp.GetType().GetFields(BindingFlags.Public |
                                                                                  BindingFlags.Instance |
                                                                                  BindingFlags.DeclaredOnly))
                    {

                        if (AmcComponent.RequiresDefinition(fieldInfo))
                        {

                            SerializedProperty requiredProperty = obj.FindProperty(fieldInfo.Name);

                            if (requiredProperty != null)
                            {
                                requiredProperties.Add(requiredProperty);
                            }
                        }
                        else {

                            SerializedProperty optionalProperty = obj.FindProperty(fieldInfo.Name);
                            if (optionalProperty != null)
                            {
                                optionalProperties.Add(optionalProperty);
                            }
                        }
                    }

                    obj.Update();

                    EditorGUILayout.LabelField("Required fields (" + requiredProperties.Count + ")");
                    EditorGUI.indentLevel++;
                    foreach (SerializedProperty required in requiredProperties)
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.PropertyField(required);
                        EditorGUILayout.EndHorizontal();
                    }
                    EditorGUI.indentLevel--;

                    EditorGUILayout.LabelField("Optional fields (" + optionalProperties.Count + ")");
                    EditorGUI.indentLevel++;
                    foreach (SerializedProperty optional in optionalProperties)
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.PropertyField(optional);
                        EditorGUILayout.EndHorizontal();
                    }
                    EditorGUI.indentLevel--;

                    //And always apply changes. This will update the serialized properties of the serialized object
                    obj.ApplyModifiedProperties();

                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("Instantiate!"))
                    {
                        //GameObject newGo = Instantiate(tmpGo);
                        //newGo.name = "New Object";
                    }

                    if (GUILayout.Button("Save"))
                    {
                        GameObject newGo = Instantiate(tmpGo);
                        AmcComponent[] comps = newGo.GetComponents<AmcComponent>();
                        foreach (AmcComponent cmp in comps)
                        {
                            Debug.Log("jan");
                            File.WriteAllText("jan.txt", cmp.GenerateComponentScript());
                        }
                    }
                    GUILayout.EndHorizontal();
                    // DestroyImmediate(tmpGo);
                }
            }
            GUILayout.EndVertical();
        }
    }
}
