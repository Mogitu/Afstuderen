/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Declare required functions for the gamemanager.
/// </summary>
public interface IMainManager
{
    void StartPracticeRound();
    void StartMultiplayerHost();
    void StartMultiplayerClient();
    void RestartGame();
    void QuitApplication();
}

