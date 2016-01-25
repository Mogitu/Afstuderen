using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Concrete presenter, used to open/close or changeviews. Childs should add extra logic.
/// </summary>
public abstract class Presenter: MonoBehaviour, IPresenter {

    public List<View> views; 
   
    /// <summary>
    /// Change the view based on an attached, concrete, view implementation.
    /// </summary>
    /// <param name="view">compare with a concrete view</param>
    public void ChangeView(View view)
    {
        
    }

    /// <summary>
    /// Open selected view, closes all others.
    /// TODO    :   Comparing enum with string == bad. Needs a future fix.
    /// </summary>
    /// <param name="viewName">compare with gameobject name.</param>
    public void ChangeView(string viewName)
    {                       
        for (int i=0; i< views.Count;i++)
        {
            if (views[i].name.Equals(viewName))
            {
                views[i].gameObject.SetActive(true);
            }
            else
            {
                views[i].gameObject.SetActive(false);
            }
        }
    } 

    /// <summary>
    /// Open selected view, keeps rest open.
    /// </summary>
    /// <param name="viewName"></param>
    public void OpenView(string viewName)
    {
        for(int i=0; i< views.Count;i++)
        {
            if (views[i].name.Equals(viewName))
            {
                views[i].gameObject.SetActive(true);
                break;
            }
        }
    }

    /// <summary>
    /// Close selected view.
    /// </summary>
    /// <param name="viewName"></param>
    public void CloseView(string viewName)
    {
        for (int i = 0; i < views.Count; i++)
        {
            if (views[i].name.Equals(viewName))
            {
                views[i].gameObject.SetActive(false);
                break;
            }
        }
    }

    public GameObject GetView(string viewName)
    {
        for (int i = 0; i < views.Count; i++)
        {
            if (views[i].name.Equals(viewName))
            {
                return views[i].gameObject;  
            }
        }
        return null;
    }

    public void ToggleView(string viewName)
    {
        if (!GetView(viewName).activeSelf)
        {
            OpenView(viewName);
        }
        else
        {
            CloseView(viewName);
        }
    }
}