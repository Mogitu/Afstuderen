using UnityEngine;
/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Represents a "root view" object directly attached to a canvas with a presenter component.
/// </summary>
namespace AMC.GUI
{
    public interface IView
    {
        //void SetPresenter(IPresenter presenter);
        T GetPresenterType<T>() where T : IPresenter;       
        void ShowViewComponent(string name);
        void HideViewComponent(string name);
        void ToggleViewComponent(string name);
    }
}
   
