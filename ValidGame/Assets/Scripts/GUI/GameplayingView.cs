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
    private GuiPresenter Presenter;

    public void OpenCardView(){
        Presenter.ToggleCardbrowser();    
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Presenter.Restart();
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
            Presenter.UpdateContextInformationText(InfoText, hit);
        }
    }

    public override void SetPresenter(IPresenter presenter)
    {
        Presenter = (GuiPresenter)presenter;
    }
}