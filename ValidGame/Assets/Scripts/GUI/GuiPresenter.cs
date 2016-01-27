using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Presenter that is responsible for the main gui and underlying views.
/// TODO    :   When this class grows to big it is an idea to split the gui up with multiple presenters.
/// </summary>
public class GuiPresenter : Presenter
{

    /// <summary>
    ///Desc :   This enum needs to map to existing view names 1 on 1.
    ///TODO :   Enum is probably not the best solution, but for now workable.
    /// </summary>
    public enum VIEWS
    {
        MainmenuView,
        GamePlayingView,
        GameovermenuView,
        CardbrowserView,
        MultiplayermenuView
    }

    public MainManager mainManager;

    void Start()
    {
        foreach (View v in views)
        {
            v.SetPresenter(this);
        }
        ChangeView(VIEWS.MainmenuView.ToString());
    }

    public void StartPracticeRound()
    {
        ChangeView(VIEWS.GamePlayingView.ToString());
        mainManager.StartPracticeRound();
    }

    public void StartMultiplayerClient()
    {
        ChangeView(VIEWS.GamePlayingView.ToString());
        mainManager.StartMultiplayerClient();
    }

    //TODO  : start host instead of client.
    public void StartMultiplayerHost()
    {
        ChangeView(VIEWS.GamePlayingView.ToString());
        mainManager.StartMultiplayerClient();
    }

    public void Restart()
    {
        mainManager.Restart();
    }

    public void QuitApplication()
    {
        mainManager.QuitApplication();
    }

    public void ToggleCardbrowser()
    {
        mainManager.ToggleCameraActive();
        mainManager.ToggleAllColliders();
        ToggleView(VIEWS.CardbrowserView.ToString());      
    }

    public void ShowMainmenuView()
    {
        ChangeView(VIEWS.MainmenuView.ToString());
    }

    public void PickCard(string code)
    {
        ToggleCardbrowser();
        mainManager.PickCard(code);
    }

    public void ShowGameOverView()
    {
        ChangeView(VIEWS.GameovermenuView.ToString());
    }

    public void ShowMultiplayerView()
    {
        ChangeView(VIEWS.MultiplayermenuView.ToString());
    }

    /// <summary>
    /// Update a text with the topic description on an object, if existing.
    /// </summary>
    /// <param name="txt">The text mesh to update.</param>
    /// <param name="hit">The hit to use.</param>
    public void UpdateGuiText(Text txt, RaycastHit hit)
    {
        SubtopicDescription desc = hit.transform.gameObject.GetComponentInChildren<SubtopicDescription>();
        if (desc != null)
        {
            txt.text = desc.descriptionTxt.text;
        }
    }
}