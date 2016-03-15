using UnityEngine;
using UnityEngine.UI;
using AMC.GUI;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Default gui view for when the player is hovering over the game board.
/// </summary>
public class GameplayingView : View{

    public GameObject InfoBar;
    public Text InfoText;
    private GuiPresenter GuiPresenter;

    void Awake()
    {
        GuiPresenter = GetPresenterType<GuiPresenter>();
    }

    public void OpenCardView(){       
        GuiPresenter.ToggleCardbrowser();    
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // GuiPresenter.Restart();
            Presenter.OpenView("PauzeMenu");
        }
        CheckForDescriptionHit();
    }

    private void CheckForDescriptionHit()
    {
        //Update inforbar description text with the object below the mouse pointer.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10))
        {
            GuiPresenter.UpdateContextInformationText(InfoText, hit);
        }
    }   
}