using UnityEngine;
using UnityEngine.UI;
using AMC.GUI;

public class TeamSelectView : View {

    public Toggle PlandAndDoToggle;
    public Toggle CheckAndActToggle;
	// Use this for initialization
	void Start () {
	
	}
	
	public void ClickedGo()
    {
        TeamType teamType;
        if (PlandAndDoToggle.isOn)
        {
            teamType = TeamType.ALL;
        }
        else
        {
            teamType = TeamType.CheckAndAct;
        }
        GetPresenterType<GuiPresenter>().StartPracticeRound(teamType);
    }
}
