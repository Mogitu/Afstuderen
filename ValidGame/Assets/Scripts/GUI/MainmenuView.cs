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
#if UNITY_WEBGL
        Application.ExternalEval("window.open("+ "http://www.amicoservices.nl/" + ");");
#else
        Application.OpenURL("http://www.amicoservices.nl/");
#endif
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