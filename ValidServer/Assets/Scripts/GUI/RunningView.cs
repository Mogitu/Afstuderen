using UnityEngine;
using UnityEngine.UI;
using AMC.GUI;

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
        ((ServerPresenter)Presenter).EventManager.AddListener(ServerEvents.ClientJoined, IncreaseConnectionsCount);
        ((ServerPresenter)Presenter).EventManager.AddListener(ServerEvents.MatchCreated, IncreaseMatchCount);
        ((ServerPresenter)Presenter).EventManager.AddListener(ServerEvents.PlayerLeft, OnClientLeft);
    }

    private void IncreaseConnectionsCount(short event_Type, Component sender, object param = null) {
        NumConnections++;
        ConnectionCountTxt.text = NumConnections.ToString();
        //CreateLogText("A player connected");
    }

    private void IncreaseMatchCount(short event_Type, Component sender, object param = null)
    {
        NumMatches++;
        MatchesCount.text = NumMatches.ToString();
        //CreateLogText("A new match is created");
    }
	
    public void Disconnect()
    {
        ((ServerPresenter)Presenter).EventManager.PostNotification(ServerEvents.Disconnect,this,null);
        Presenter.ChangeView("MainView");
    }

    private void OnClientLeft(short event_Type, Component sender, object param = null)
    {
        NumMatches--;
        MatchesCount.text = NumMatches.ToString();
        NumConnections--;
        ConnectionCountTxt.text = NumConnections.ToString();
    }	

    private void CreateLogText(string message)
    {
        GameObject txt = Instantiate(LogText);
        txt.transform.SetParent(LogContent);
        txt.transform.position = new Vector2(0,0);
        txt.GetComponent<Text>().text = message;       
       //go.transform.SetParent(LogContent);
       //go.transform.position = new Vector2(0, 0);
    }
}
