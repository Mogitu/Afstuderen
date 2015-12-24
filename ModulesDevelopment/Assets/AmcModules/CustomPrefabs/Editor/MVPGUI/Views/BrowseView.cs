using UnityEngine;
using UnityEditor;

namespace AmcCustomPrefab
{
    /// <summary>
    /// View for browsing external prefab files
    /// </summary>
    class BrowseView : IView
    {
        private ResourcesPresenter presenter;

        public BrowseView(ResourcesPresenter presenter)
        {
            this.presenter = presenter;
        }

        public void Display()
        {            
            if(GUILayout.Button("Browse"))
            {
                presenter.BrowseAndLoad(EditorUtility.OpenFilePanel("", "", "txt"));
            }
        }
    }
}
