using AMC.GUI;
/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Main menu. Start, quit, etc.
/// </summary>
public class MainmenuView : View {

    private GuiPresenter Presenter;

    public void ClickedStart()
    {          
        Presenter.StartPracticeRound();            
    }

    public void ClickedMultiplayer()
    {
        Presenter.ShowMultiplayerView();  
    }

    public void ClickedQuit()
    {
        Presenter.QuitApplication();
    }

    public override void SetPresenter(IPresenter presenter)
    {
        this.Presenter = (GuiPresenter)presenter;
    }
}