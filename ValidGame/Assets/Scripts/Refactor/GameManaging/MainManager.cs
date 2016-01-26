using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Hooks all primary functionalities/modules together, after initializing them.
/// TODO    :   In order to comply better with SOLID, this probably should implement an interface.
/// </summary>
public class MainManager : MonoBehaviour
{
    public CameraController cameraController;   
    private GameStateManager gamestateManager;
    private CardManager cardManager;
    private int score;

    void Awake()
    {
        gamestateManager = new GameStateManager(this);
        cardManager = new CardManager(this);
    }

    void Start()
    {        
        score = 0;
    }

    void Update()
    {      
        gamestateManager.UpdateCurrentState();
        cardManager.ManageCards();
    }

    public void StartMultiplayer()
    {
        gamestateManager.SetMultiplayerState();
    }

    public void StartPracticeRound()
    {
        cameraController.RunGameStartAnimation();
        gamestateManager.SetPlayingState();
    }

    public void EndGame()
    {
        cameraController.RunGameEndAnimation();
        gamestateManager.SetGameoverState();        
    }

    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void PickCard(string code)
    {
        cardManager.SelectCard(code);        
    }

    public void QuitApplication()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
}
