using UnityEngine;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Abstract view implementation.......
/// </summary>
public abstract class View : MonoBehaviour, IView
{
    public abstract void SetPresenter(IPresenter presenter);   
    public virtual void Awake()
    {
        //Retreive the presenter on the root canvas object and set it.
        IPresenter tmp = GetComponentInParent<IPresenter>();
        SetPresenter(tmp);
    }
}

