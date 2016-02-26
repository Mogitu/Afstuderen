using UnityEngine.UI;
using AMC.GUI;
/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Multiplayerview
/// </summary>
public class MultiplayermenuView : View {

    public Text IpAdress;
    private GuiPresenter Presenter;

    public void ClickedClient()
    {
        Presenter.StartMultiplayerClient(IpAdress.text);
    }

    public void ClickedHost()
    {
        Presenter.StartMultiplayerHost(IpAdress.text);        
    }

    public void GoBack()
    {
        Presenter.ShowMainmenuView();
    }

    public override void SetPresenter(IPresenter presenter)
    {
        Presenter = (GuiPresenter)presenter;
    }
}