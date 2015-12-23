using UnityEngine;
using System.Collections;

namespace AmcCustomPrefab
{
    public class CreationPresenter {
        private IView scriptView;
        private IView selectView;
        private int selGridInt;
        private string[] selStrings = new string[] { "Select it!", "Script it!" };
        public CreationPresenter()
        {
            scriptView = new ScriptView();
            selectView = new SelectView();
        }

        public void Show()
        {
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical("Box");
            selGridInt = GUILayout.SelectionGrid(selGridInt, selStrings, 1, GUILayout.Width(100));
            GUILayout.EndVertical();
            GUILayout.BeginVertical("Box");
            switch (selGridInt)
            {
                case 0:
                    selectView.Display();
                    break;
                case 1:
                    scriptView.Display();
                    break;
            }
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }
    }
}
