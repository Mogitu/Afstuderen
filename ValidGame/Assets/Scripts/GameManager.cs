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

    public void HostGame()
    {
        StartServer();
        mainMenu.SetActive(false);
        gameMenu.SetActive(true);
        RunAnimation();
    }

    public void JoinGame()
    {
        StartClient();
        mainMenu.SetActive(false);
        gameMenu.SetActive(true);
        RunAnimation();
    }

    public void RunAnimation()
    {
        camAnimator.SetBool("GameStarted", true);
    }
}
