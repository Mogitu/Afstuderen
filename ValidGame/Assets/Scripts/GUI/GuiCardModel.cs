using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GuiCardModel : MonoBehaviour, IModel
{
    public string matchCode = "1a";
    public GuiPresenter presenter;

    public void SetPresenter(GuiPresenter presenter)
    {
        this.presenter = presenter;
    }
}

