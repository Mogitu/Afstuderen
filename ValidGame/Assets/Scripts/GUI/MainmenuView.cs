/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Main menu. Start, quit, etc.
/// </summary>
public class MainmenuView : View {

    private GuiPresenter presenter;

    public void ClickedStart()
    {          
        presenter.StartPracticeRound();            
    }

    public void ClickedMultiplayer()
    {
        presenter.ShowMultiplayerView();  
    }

    public void ClickedQuit()
    {
        presenter.QuitApplication();
    }

    public override void SetPresenter(IPresenter presenter)
    {
        this.presenter = (GuiPresenter)presenter;
    }
}