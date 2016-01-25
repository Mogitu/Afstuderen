using UnityEngine;

public class MainManager : MonoBehaviour {
    public GameState gameState;
  //  public PlayingStateOld playingState;
   // public GameoverStateOld gameOverState;
   // public WaitingStateOld waitingState;
    public int score;

    void Awake()
    {
      //  playingState = new PlayingStateOld(null);
       // gameOverState = new GameoverStateOld(null);
       // waitingState = new WaitingStateOld(null);
    }

	// Use this for initialization
	void Start () {
       // gameState = waitingState;
        score=0;
	}
	
	// Update is called once per frame
	void Update () {
       // gameState.UpdateState();
	}

    public void StartMultiplayer(){
           
    }

    public void StartPracticeRound()
    {
       // gameState = playingState;
        Debug.Log("Start practice");
    }

    public void QuitApplication()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
}
