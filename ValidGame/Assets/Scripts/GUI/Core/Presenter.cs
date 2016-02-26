using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Concrete presenter, used to open/close or changeviews. Childs should add extra logic.
/// </summary>

namespace AMC.GUI
{
    public abstract class Presenter : MonoBehaviour, IPresenter
    {
        private Dictionary<string, GameObject> Views;
        void Awake()
        {
            Views = GetAllViews();
        }

        /// <summary>
        /// Change the view based on an attached, concrete, view implementation.
        /// </summary>
        /// <param name="view">compare with a concrete view</param>
        public void ChangeView(IView view)
        {
            //Implement
        }

        /// <summary>
        /// Open selected view, closes all others.  
        /// </summary>
        /// <param name="viewName">compare with gameobject name.</param>
        public void ChangeView(string viewName)
        {
            foreach (KeyValuePair<string, GameObject> go in Views)
            {
                if (go.Key == viewName)
                {
                    go.Value.SetActive(true);
                }
                else {
                    go.Value.SetActive(false);
                }
            }
        }

        /// <summary>
        /// Open selected view, keeps rest open.
        /// </summary>
        /// <param name="viewName"></param>
        public void OpenView(string viewName)
        {
            Views[viewName].SetActive(true);
        }

        /// <summary>
        /// Close selected view.
        /// </summary>
        /// <param name="viewName"></param>
        public void CloseView(string viewName)
        {
            Views[viewName].SetActive(false);
        }

        /// <summary>
        /// Retreive a view
        /// </summary>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public GameObject GetView(string viewName)
        {
            return Views[viewName];
        }

        /// <summary>
        /// Toggles visibility and active status of a view.
        /// </summary>
        /// <param name="viewName"></param>
        public void ToggleView(string viewName)
        {
            GameObject go = GetView(viewName);
            if (!go.activeSelf)
            {
                OpenView(viewName);
            }
            else
            {
                CloseView(viewName);
            }
        }

        public Dictionary<string, GameObject> GetAllViews()
        {
            View[] views = GetComponentsInChildren<View>(true);
            Dictionary<string, GameObject> tmp = new Dictionary<string, GameObject>();
            for (int i = 0; i < views.Length; i++)
            {
                tmp.Add(views[i].name, views[i].gameObject);
            }
            return tmp;
        }
    }
}