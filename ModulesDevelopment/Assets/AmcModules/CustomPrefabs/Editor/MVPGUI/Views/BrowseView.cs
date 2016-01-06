using UnityEngine;
using UnityEditor;

namespace AmcCustomPrefab
{
    /// <summary>
    ///Author   :   Maikel van Munsteren.
    ///Desc     :   View for displaying the browser.
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
