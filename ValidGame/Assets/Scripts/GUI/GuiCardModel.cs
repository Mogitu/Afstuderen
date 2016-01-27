using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :
/// </summary>
public class GuiCardModel : MonoBehaviour, IModel
{
    public string matchCode = "1a";
    public GuiPresenter presenter;

    public void SetPresenter(GuiPresenter presenter)
    {
        this.presenter = presenter;
    }
}

