using UnityEngine;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Main menu. Start, quit, etc.
/// </summary>
public class MainmenuView : MonoBehaviour, IView {

    private GuiPresenter presenter;
    public void SetPresenter(Presenter presenter)
    {
        this.presenter = (GuiPresenter)presenter;
    }

    void Awake()
    {
        Presenter tmp = GetComponentInParent<Presenter>();
        SetPresenter(tmp);
    }   

    public void ClickedStart()
    {
        gameObject.SetActive(false);
        presenter.StartPracticeRound();        
        Debug.Log("start");
    }

    public void ClickedMultiplayer()
    {
        gameObject.SetActive(false);
        Debug.Log("Multiplayer");
    }

    public void ClickedQuit()
    {
        presenter.QuitApplication();
    }
}
