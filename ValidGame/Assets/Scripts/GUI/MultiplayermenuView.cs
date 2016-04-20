using UnityEngine.UI;
using AMC.GUI;
/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Multiplayerview
/// </summary>
public class MultiplayermenuView : View {

    //public Text IpAdress;
    public Text PlayerName;
    private GuiPresenter GuiPresenter;


    void Awake()
    {
        GuiPresenter = GetPresenterType<GuiPresenter>();
    }

    public void ClickedClient()
    {
        GuiPresenter.StartMultiplayerClient(PlayerName.text);
    }    

    public void GoBack()
    {
        GuiPresenter.ShowMainmenuView();
    }   
}