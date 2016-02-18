using UnityEngine;

public class MenuView : View
{
    private GuiPresenter presenter;
    public override void SetPresenter(IPresenter presenter)
    {
        this.presenter = (GuiPresenter)presenter;
    }

    public void StartServer()
    {
        Debug.Log("Starting server");
        presenter.ShowRunningView();
    }

    public void StopServer()
    {
        Debug.Log("Stopping server");
    }
}
