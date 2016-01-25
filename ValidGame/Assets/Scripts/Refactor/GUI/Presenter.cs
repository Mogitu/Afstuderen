using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Concrete presenter
/// </summary>
public abstract class Presenter: MonoBehaviour, IPresenter {

    public List<View> views;
    private View currentview;
    
    /// <summary>
    /// Change the view based on an attached, concrete, view implementation.
    /// </summary>
    /// <param name="view">compare with a concrete view</param>
    public void ChangeView(View view)
    {
        this.currentview = view;
    }

    /// <summary>
    /// Change the view based on a string compare.
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
}