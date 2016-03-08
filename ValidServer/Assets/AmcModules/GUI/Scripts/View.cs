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
        //public abstract void SetPresenter(IPresenter presenter);

        /*
        void Awake()
        {
            //Retreive the presenter on the root canvas object and set it.
            //IPresenter tmp = GetComponentInParent<IPresenter>();
            //SetPresenter(tmp);
        }
        */

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

        protected T GetPresenterType<T>() where T : IPresenter
        {
            //IPresenter tmp = GetComponentInParent<IPresenter>();
            // _Presenter = tmp;
            //return (T)Convert.ChangeType(tmp, typeof(T)); ;
            return (T)GetComponentInParent<IPresenter>();
        }
    }
}