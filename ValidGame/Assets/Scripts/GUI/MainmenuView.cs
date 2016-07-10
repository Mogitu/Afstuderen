using AMC.GUI;
using UnityEngine;
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

    public void OpenAmcHomePage()
    {
        Application.OpenURL("http://www.amicoservices.nl/");
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