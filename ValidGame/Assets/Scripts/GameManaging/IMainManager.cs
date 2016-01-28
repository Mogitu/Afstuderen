/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Declare required functions for the gamemanager.
/// </summary>
public interface IMainManager
{
    void StartMultiplayerHost(string ip);
    void StartMultiplayerClient(string ip);
    void RestartGame();
    void QuitApplication();
}

