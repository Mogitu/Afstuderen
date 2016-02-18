using AMC.GUI;

public class MenuView : View
{
    private GuiPresenter presenter;
    public override void SetPresenter(IPresenter presenter)
    {
        this.presenter = (GuiPresenter)presenter;
    }

    public void StartServer()
    {
        presenter.StartServer();
    }

    public void QuitApp()
    {
        presenter.QuitApp();
    }
}