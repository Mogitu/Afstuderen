using AMC.GUI;

public class RunningView : View
{
    private GuiPresenter presenter;
    public override void SetPresenter(IPresenter presenter)
    {
        this.presenter = (GuiPresenter)presenter;
    }   
}