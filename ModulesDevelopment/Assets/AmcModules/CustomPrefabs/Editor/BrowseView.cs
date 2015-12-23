using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace AmcCustomPrefab
{
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
                presenter.Browse();
            }
        }
    }
}
