using UnityEngine;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Displays game over items, like scores etc.
/// </summary>
public class GameovermenuView : MonoBehaviour, IView {
    private GuiPresenter presenter;
    public void SetPresenter(Presenter presenter)
    {
        this.presenter = (GuiPresenter)presenter;
    }   
}
