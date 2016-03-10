using UnityEngine;
using UnityEngine.UI;
using AMC.GUI;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   
/// </summary>
public class RunningView : View {

    [SerializeField]
    private Text ConnectionCountTxt;
    [SerializeField]
    private Text MatchesCount;
    [SerializeField]
    private Transform LogContent;
    [SerializeField]
    private GameObject LogText;

    private int NumMatches;
    private int NumConnections;

    // Use this for initialization
    void Start() {
        NumConnections = 0;
        NumMatches = 0;
        ((ServerPresenter)Presenter).EventManager.AddListener(ServerEvents.ClientJoined, UpdateStats);
        ((ServerPresenter)Presenter).EventManager.AddListener(ServerEvents.MatchCreated, UpdateStats);
        ((ServerPresenter)Presenter).EventManager.AddListener(ServerEvents.PlayerLeft, UpdateStats);
    }
   
    private void UpdateStats(short event_Type, Component sender, object param = null)
    {
        int[] stats = (int[])param;
        NumConnections = stats[0];
        NumMatches = stats[1];
        MatchesCount.text = NumMatches.ToString();
        ConnectionCountTxt.text = NumConnections.ToString();
    }
	
    public void Disconnect()
    {
        ((ServerPresenter)Presenter).EventManager.PostNotification(ServerEvents.Disconnect,this,null);
        Presenter.ChangeView("MainView");
    }   
}
