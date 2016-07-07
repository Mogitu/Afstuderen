using AMC.GUI;
/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Main menu. Start, quit, etc.
/// </summary>
public class MainmenuView : View
{
    private GuiPresenter GuiPresenter;

    void Awake()
    {
        GuiPresenter = GetPresenterType<GuiPresenter>();
    }

    void Start()
    {
#if UNITY_WEBGL
        GetViewComponent("QuitButton").SetActive(false);
#endif
    }

    public void ClickedStart()
    {
        // GuiPresenter.ShowTeamSelectView();
        GuiPresenter.StartPracticeRound();
    }

    public void ClickedMultiplayer()
    {
        GuiPresenter.ShowMultiplayerView();
    }

    public void ClickedQuit()
    {
        GuiPresenter.QuitApplication();
    }

    public void ShowOptions()
    {
        GuiPresenter.OpenOptionView();
    }
}