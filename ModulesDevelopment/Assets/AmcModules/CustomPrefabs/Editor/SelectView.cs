using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using System.IO;

namespace AmcCustomPrefab
{
    class SelectView : IView
    {
        private MonoScript script;
        private List<SerializedProperty> requiredProperties;
        private List<SerializedProperty> optionalProperties;
        private AmcComponent tmpComp;
        private GameObject tmpGo;

        public void Display()
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
                    GameObject newGo = GameObject.Instantiate(tmpGo);
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
    }
}