using UnityEngine;
using AMC.GUI;

public class MainView : View {


    public void Quit()
    {
        ((ServerPresenter)Presenter).EventManager.PostNotification(ServerEvents.QuitApplication,this,null);
    }
	
}
