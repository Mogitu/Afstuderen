using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using AMC.GUI;

public class RunningView : View {

    [SerializeField]
    private Text ConnectionCountTxt; 

    private int Connections;

 


    // Use this for initialization
    void Start() {
        Connections = 0;
        ((ServerPresenter)Presenter).EventManager.AddListener(ServerEvents.ClientJoined, IncreaseConnectionsCount);
    }


    private void IncreaseConnectionsCount(short event_Type, Component sender, object param = null) {
        Connections++;
        ConnectionCountTxt.text = Connections.ToString();
    }

	
	// Update is called once per frame
	void Update () {
	
	}
}
