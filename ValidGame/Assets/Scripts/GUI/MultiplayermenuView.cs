/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Multiplayerview
/// </summary>
public class MultiplayermenuView : View {

    public void ClickedClient()
    {
        presenter.StartMultiplayerClient();
    }

    public void ClickedHost()
    {
        presenter.StartMultiplayerHost();
    }

    public void GoBack()
    {
        presenter.ShowMainmenuView();
    }	
}