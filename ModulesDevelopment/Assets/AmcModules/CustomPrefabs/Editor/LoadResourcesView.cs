using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace AmcCustomPrefab
{
    class LoadResourcesView : IView
    {
        private ResourcesPresenter presenter;

        public LoadResourcesView(ResourcesPresenter presenter)
        {
            this.presenter = presenter;
        }


        public void Display()
        {
            if (GUILayout.Button("Load All"))
            {
                presenter.LoadAll();
            }
        }
    }
}
