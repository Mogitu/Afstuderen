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

            EditorGUILayout.LabelField("Required fields (" + presenter.model.RequiredProperties.Count + ")");
            EditorGUI.indentLevel++;
            foreach (SerializedProperty required in presenter.model.RequiredProperties)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(required);
                EditorGUILayout.EndHorizontal();
            }
            EditorGUI.indentLevel--;

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
                presenter.SaveSelectedModel();
            }
            GUILayout.EndHorizontal();
            // DestroyImmediate(tmpGo);
        }
    }
}
