using System;
using UnityEngine;



public abstract class View : MonoBehaviour, IView
{
    protected GuiPresenter presenter;

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

