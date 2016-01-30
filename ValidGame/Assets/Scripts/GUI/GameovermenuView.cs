using UnityEngine;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Displays game over items, like scores etc.
/// </summary>
public class GameovermenuView : View {

    public override void Awake()
    {
        base.Awake();
        presenter.eventManager.AddListener(EVENT_TYPE.RECEIVESCORE, OnScoreReceived);
    }
    public void Restart()
    {
        presenter.Restart();
    }    

    public void OnScoreReceived(EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        Debug.Log("Received score of " + Param.ToString());
    }
}
