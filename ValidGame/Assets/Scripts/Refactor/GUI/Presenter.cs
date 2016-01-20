using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Attach implemenation of this type to a canvas root object to make it an presenter, and get access to its underlying views.
/// </summary>
public abstract class Presenter: MonoBehaviour {

    private IView currentview;
    private List<IView> views;  

    public virtual void Awake()
    {
        views = GetAllViews();
    }  

    public void ChangeView(IView view)
    {
        this.currentview = view;
    }

    public void ChangeView(string viewName)
    {
        
    }

    /// <summary>
    /// Non recursive search on child objects(views) on this presenter.
    /// </summary>
    /// <returns>Views attached to this presenter.</returns>
    private List<IView> GetAllViews()
    {
        //Create temporary array and set each presenter before adding them into a list.
        IView[] tmpViews = GetComponentsInChildren<IView>();
        List<IView> tmpList = new List<IView>();
        for (int i = 0; i < tmpViews.Length; i++)
        {
            tmpViews[i].SetPresenter(this);                    
            tmpList.Add(tmpViews[i]);
            Debug.Log(tmpViews[i].ToString());  
        }
        return tmpList;
    }
}