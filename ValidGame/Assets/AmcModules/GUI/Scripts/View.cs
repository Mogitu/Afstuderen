using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Abstract view implementation.......
/// </summary>
namespace AMC.GUI
{
    public abstract class View : MonoBehaviour, IView
    {
        private IPresenter _Presenter;
        private Dictionary<string, GameObject> ViewComponents;
        //public abstract void SetPresenter(IPresenter presenter);


        void Awake()
        {
            //Retreive the presenter on the root canvas object and set it.
            //IPresenter tmp = GetComponentInParent<IPresenter>();
            //SetPresenter(tmp);
            ViewComponents = PopulateViewComponents();
        }


        private Dictionary<string, GameObject> PopulateViewComponents()
        {
            RectTransform[] objects = GetComponentsInChildren<RectTransform>();
            Dictionary<string, GameObject> tmpDict = new Dictionary<string, GameObject>();

            for (int i = 0; i < objects.Length; i++)
            {
                tmpDict.Add(objects[i].name, objects[i].gameObject);
            }
            return tmpDict;
        }

        public IPresenter Presenter
        {
            get
            {
                if (_Presenter == null)
                {
                    _Presenter = GetComponentInParent<IPresenter>();
                }
                return _Presenter;
            }
            protected set
            {
                _Presenter = value;
            }
        }

        public T GetPresenterType<T>() where T : IPresenter
        {            
            return (T)GetComponentInParent<IPresenter>();
        }

        public GameObject GetViewComponent(string name)
        {
            return ViewComponents[name].gameObject;
        }

        public void ShowViewComponent(string name)
        {
            GetViewComponent(name).SetActive(true);
        }

        public void HideViewComponent(string name)
        {
            GetViewComponent(name).SetActive(false);
        }

        public void ToggleViewComponent(string name)
        {
            GameObject go = GetViewComponent(name);
            go.SetActive(!go.activeSelf);
        }
    }
}