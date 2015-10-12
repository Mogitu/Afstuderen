using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using System.Collections;

public class GameManager : NetworkManager{

    // Use this for initialization  
    private GameObject mainCam;
    private Animator camAnimator;
    private bool gameStarted;
    public GameObject gameMenu;
    public GameObject mainMenu;
    public PopupHandler popupHandler;
    public Text ipAdress;
    public InputField chatInput;
    public Text chatField;

	void Start () {
        mainCam = Camera.main.gameObject;
        camAnimator = mainCam.GetComponent<Animator>();       
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Return))
        {
            chatField.text += chatInput.text+"\n";
            chatInput.text = "";
           //StringMessage msgs = new StringMessage("testmessage");
           //client.Send()
        }      
	} 

    public void HostGame()
    {        
        StartHost();    
        mainMenu.SetActive(false);
        gameMenu.SetActive(true);
        RunAnimation();
        popupHandler.Show("Hosting game");       
    }

    public void JoinGame()
    {
        networkAddress = ipAdress.text;
        StartClient();
        Network.Connect(ipAdress.text,7777);
        mainMenu.SetActive(false);
        gameMenu.SetActive(true);
        RunAnimation();
        popupHandler.Show("Joining game");
    }

    public void RunAnimation()
    {
        camAnimator.SetBool("GameStarted", true);
    }
}
