using UnityEngine;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   exposes all required functionality needed for a presenter implementation
/// </summary>
interface IPresenter
{
    void ChangeView(View view);
    void ChangeView(string viewName);
    void OpenView(string viewName);
    void CloseView(string viewName);
    GameObject GetView(string viewName);
    void ToggleView(string viewName);
}
    

