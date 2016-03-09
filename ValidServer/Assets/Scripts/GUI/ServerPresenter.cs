using UnityEngine;
using System.Collections;
using AMC.GUI;

public class ServerPresenter : Presenter {    
    public EventManager EventManager;

    public void StartServer()
    {
        ChangeView("RunningView");
        EventManager.PostNotification(ServerEvents.StartServer, this, null);
    }   
}
