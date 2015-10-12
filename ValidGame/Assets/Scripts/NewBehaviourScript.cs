using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NewBehaviourScript : NetworkManager{

	// Use this for initialization
	void Start () {
        StartHost();
        Application.LoadLevel("GameScene");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnServerConnected()
    {
        Debug.Log("fgfdgdfgdf");
    }
}
