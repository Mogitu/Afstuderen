using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Reflection;

/// <summary>
/// Custom inspector added to AmcComponents to generate scripts from their setup.
/// </summary>
[CustomEditor(typeof(AmcComponent), true)]
public class CustomPrefabEditor : Editor
{

    List<SerializedProperty> requiredProperties;
    List<SerializedProperty> optionalProperties;
    AmcComponent advancedComponent;


    string generatedScript = "";

    void OnEnable()
    {
        advancedComponent = serializedObject.targetObject as AmcComponent;
        requiredProperties = new List<SerializedProperty>();
        optionalProperties = new List<SerializedProperty>();

        foreach (FieldInfo fieldInfo in target.GetType().GetFields(BindingFlags.Public |
                                                                  BindingFlags.Instance |
                                                                  BindingFlags.DeclaredOnly))
        {
            if (AmcComponent.RequiresDefinition(fieldInfo))
            {
                SerializedProperty requiredProperty = serializedObject.FindProperty(fieldInfo.Name);
                if (requiredProperty != null)
                {
                    requiredProperties.Add(requiredProperty);
                }
            }
            else {
                SerializedProperty optionalProperty = serializedObject.FindProperty(fieldInfo.Name);
                if (optionalProperty != null)
                {
                    optionalProperties.Add(optionalProperty);
                }
            }
        }
    }

    public override void OnInspectorGUI()
    {
        //Always update the object
        serializedObject.Update();

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

        if (GUILayout.Button("Generate script"))
        {
            generatedScript = advancedComponent.GenerateComponentScript();
        }
        if (generatedScript.Length > 0)
        {
            EditorGUILayout.TextArea(generatedScript);
        }

        //And always apply changes. This will update the serialized properties of the serialized object
        serializedObject.ApplyModifiedProperties();
    }
}