using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Default gui view for when the player is hovering over the game board.
/// </summary>
public class GameplayingView : View{

    public GameObject infoBar;
    public Text infoText;

    public void OpenCardView(){
        presenter.ToggleCardbrowser();    
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            presenter.Restart();
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
            presenter.UpdateContextInformationText(infoText, hit);
        }
    }
}