using UnityEngine;
using System.Collections;

public class MainManager : MonoBehaviour {

    public GameState gameState;
    public PlayingState playingState;
    public GameoverState gameOverState;
    public WaitingState waitingState;

    void Awake()
    {
        playingState = new PlayingState(null);
        gameOverState = new GameoverState(null);
        waitingState = new WaitingState(null);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        gameState.UpdateState();
	}

    public void StartMultiplayer(){
           
    }

    public void StartPracticeRound()
    {
        Debug.Log("Start practice");
    }

    public void QuitApplication()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
}
