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

        void Awake()
        {          
            ViewComponents = PopulateViewComponents();
        }

        void Start()
        {
            //ViewComponents = PopulateViewComponents();
        }

        void OnEnable()
        {            
            if (ViewComponents == null)
            {
                ViewComponents = PopulateViewComponents();               
            }
        }

        private Dictionary<string, GameObject> PopulateViewComponents()
        {           
            Dictionary<string, GameObject> tmpDict = new Dictionary<string, GameObject>();
            foreach(Transform child in transform)
            {
                tmpDict.Add(child.name, child.gameObject);              
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