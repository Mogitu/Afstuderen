using UnityEngine;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Abstract view implementation.......
/// </summary>
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

    public virtual void Awake()
    {
        //Retreive the presenter on the root canvas object and set it.
        Presenter tmp = GetComponentInParent<Presenter>();
        SetPresenter(tmp);
    }    
}

