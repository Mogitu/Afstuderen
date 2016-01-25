using System;
using UnityEngine;

public abstract class View : MonoBehaviour, IView
{
    protected GuiPresenter presenter;

    public bool IsActive
    {
        get
        {
            return gameObject.activeSelf;
        }        
    }

    public void SetPresenter(Presenter presenter)
    {
        this.presenter = (GuiPresenter)presenter;
    }

    void Awake()
    {
        Presenter tmp = GetComponentInParent<Presenter>();
        SetPresenter(tmp);
    }    
}

