using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Multiplayerview
/// </summary>
public class MultiplayermenuView : View {

    public Text ipAdress;
    private GuiPresenter presenter;

    public void ClickedClient()
    {
        presenter.StartMultiplayerClient(ipAdress.text);
    }

    public void ClickedHost()
    {
        presenter.StartMultiplayerHost(ipAdress.text);        
    }

    public void GoBack()
    {
        presenter.ShowMainmenuView();
    }

    public override void SetPresenter(IPresenter presenter)
    {
        this.presenter = (GuiPresenter)presenter;
    }
}