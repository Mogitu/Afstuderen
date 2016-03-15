using UnityEngine;
using UnityEngine.UI;
using AMC.GUI;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   View to edit game settings
/// TODO    :   Probably use a single point/model for all setting values, as opposed to using similar objects in other files.
/// </summary>
public class OptionsView : View
{
    public Slider ZoomSlider;
    public Slider MoveSlider;
    public Slider LookSlider;

    //settings values
    private float ZoomSpeed;
    private float MoveSpeed;
    private float LookSpeed;

    void Awake()
    {       
        ZoomSlider.value = PlayerPrefs.GetFloat("ZoomSpeed") != 0 ? PlayerPrefs.GetFloat("ZoomSpeed") : 5;
        MoveSlider.value = PlayerPrefs.GetFloat("MoveSpeed") != 0 ? PlayerPrefs.GetFloat("MoveSpeed") : 5;
        LookSlider.value = PlayerPrefs.GetFloat("LookSpeed") != 0 ? PlayerPrefs.GetFloat("LookSpeed") : 5;
    }

    void Update()
    {
        ZoomSpeed = ZoomSlider.value;
        MoveSpeed = MoveSlider.value;
        LookSpeed = LookSlider.value;
    }

    private void SavePrefs()
    {
        PlayerPrefs.SetFloat("ZoomSpeed", ZoomSpeed);
        PlayerPrefs.SetFloat("MoveSpeed", MoveSpeed);
        PlayerPrefs.SetFloat("LookSpeed", LookSpeed);
        PlayerPrefs.Save();
    }

    public void GoBack()
    {
        SavePrefs();
        Presenter.ChangeView("MainmenuView");
    }
}

