using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace AmcCustomPrefab
{
    /// <summary>
    /// Author  :   Maikel van Munsteren.
    /// Desc    :   View for build of objects bij selection.
    /// </summary>
    public class SelectView : IView
    {
        private CreationPresenter presenter;

        public SelectView(CreationPresenter presenter)
        {
            this.presenter = presenter;
        }

        public void Display()
        {
            presenter.SetScript(EditorGUILayout.ObjectField("Script", presenter.GetScript(), typeof(MonoScript), false) as MonoScript);

            //work on obj
            presenter.WorkOnScript();
            //end

            //Formats display for all required fields found on the model
            EditorGUILayout.LabelField("Required fields (" + presenter.model.RequiredProperties.Count + ")");
            EditorGUI.indentLevel++;
            foreach (SerializedProperty required in presenter.model.RequiredProperties)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(required);
                EditorGUILayout.EndHorizontal();
            }
            EditorGUI.indentLevel--;

            //Formats display for all optional fields found on the model
            EditorGUILayout.LabelField("Optional fields (" + presenter.model.OptionalProperties.Count + ")");
            EditorGUI.indentLevel++;
            foreach (SerializedProperty optional in presenter.model.OptionalProperties)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(optional);
                EditorGUILayout.EndHorizontal();
            }
            EditorGUI.indentLevel--;

            //And always apply changes. This will update the serialized properties of the serialized object
            //obj.ApplyModifiedProperties();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Instantiate!"))
            {
                //GameObject newGo = Instantiate(tmpGo);
                //newGo.name = "New Object";
            }

            if (GUILayout.Button("Save"))
            {
                presenter.SaveSelectedModel(EditorUtility.SaveFilePanel("t", "", "", "txt"));
            }
            GUILayout.EndHorizontal();
            // DestroyImmediate(tmpGo);
        }
    }
}
