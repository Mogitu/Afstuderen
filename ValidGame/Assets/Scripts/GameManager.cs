using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GameManager : NetworkManager{

    // Use this for initialization
    private GameObject mainCam;
    private Animator camAnimator;
    private bool gameStarted;
    public GameObject gameMenu;
    public GameObject mainMenu;

	void Start () {
        mainCam = Camera.main.gameObject;
        camAnimator = mainCam.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClientConnect()
    {
        Debug.Log("Entered match");
       
       
    }

    public void OnConnectedToServer()
    {
        Debug.Log("Player connected");
        mainMenu.SetActive(true);
    }

    public void HostGame()
    {
        StartHost();
        NetworkServer.Listen(7777);
        mainMenu.SetActive(false);
        gameMenu.SetActive(true);
        RunAnimation();
    }

    public void JoinGame()
    {
        StartClient();
        Network.Connect("127.0.0.1",7777);
        mainMenu.SetActive(false);
        gameMenu.SetActive(true);
        RunAnimation();
    }

    public void RunAnimation()
    {
        camAnimator.SetBool("GameStarted", true);
    }
}
