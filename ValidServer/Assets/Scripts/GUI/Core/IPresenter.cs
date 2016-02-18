using UnityEngine;
using System.Collections.Generic;
/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   exposes all required functionality needed for a presenter implementation
/// </summary>
namespace AMC.GUI
{
    public interface IPresenter
    {
        Dictionary<string, GameObject> GetAllViews();
        void ChangeView(IView view);
        void ChangeView(string viewName);
        void OpenView(string viewName);
        void CloseView(string viewName);
        GameObject GetView(string viewName);
        void ToggleView(string viewName);
    }
}