﻿using UnityEngine;
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

    private GuiPresenter GuiPresenter;

    void Awake()
    {
        GuiPresenter = GetPresenterType<GuiPresenter>();
        SetDefaults();
    }

    /// <summary>
    /// Set default values for all sliders
    /// </summary>
    private void SetDefaults()
    {
        //When the values in the playerprefs are not 0 we use those to initialize the slider values, else we revert to hardcoded values.
        //TODO: Move hardcoded values to the config file later.
        ZoomSlider.value = PlayerPrefs.GetFloat("ZoomSpeed") != 0 ? PlayerPrefs.GetFloat("ZoomSpeed") : 15;
        MoveSlider.value = PlayerPrefs.GetFloat("MoveSpeed") != 0 ? PlayerPrefs.GetFloat("MoveSpeed") : 5;
        LookSlider.value = PlayerPrefs.GetFloat("LookSpeed") != 0 ? PlayerPrefs.GetFloat("LookSpeed") : 5;
    }

    void Update()
    {
        //Rather not have this in update; TODO; lower update interval or use events.
        ZoomSpeed = ZoomSlider.value;
        MoveSpeed = MoveSlider.value;
        LookSpeed = LookSlider.value;
    }

    private void OnDisable()
    {
        SavePrefs();
    }

    /// <summary>
    /// Save all available options to the playerprefs
    /// </summary>
    private void SavePrefs()
    {
        PlayerPrefs.SetFloat("ZoomSpeed", ZoomSpeed);
        PlayerPrefs.SetFloat("MoveSpeed", MoveSpeed);
        PlayerPrefs.SetFloat("LookSpeed", LookSpeed);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Saves the playerprefs and changes the view
    /// </summary>
    public void GoBack()
    {
        SavePrefs();
        GuiPresenter.EventManager.PostNotification(GameEvents.UpdateSettings, this, null);
        Presenter.ChangeView(VIEWS.MainmenuView);
    }
}

