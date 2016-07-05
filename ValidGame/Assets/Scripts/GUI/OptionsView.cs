using UnityEngine;
using UnityEngine.UI;
using AMC.GUI;
using UnityEngine.SceneManagement;
using System;

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
    public Dropdown SceneSelectDropDown;
    public InputField GameTimeInput;
    public Toggle ShowCoordinateToggle;

    //settings values
    private float ZoomSpeed;
    private float MoveSpeed;
    private float LookSpeed;
    private int ShowCoordinates;

    private GuiPresenter GuiPresenter;
    private const string InputFieldErrorText = "Enter a valid number";

    void Awake()
    {
        GuiPresenter = GetPresenterType<GuiPresenter>();
        SetDefaults();
        if (SceneManager.GetActiveScene().name == "Modern")
        {
            SceneSelectDropDown.value = 0;
        }
        else
        {
            SceneSelectDropDown.value = 1;
        }
    }

    private void ChangeGameTime()
    {
        int time;
        bool result = int.TryParse(GameTimeInput.text, out time);
        if (!result)
        {          
            throw new FormatException();            
        }
        GameTimeInput.textComponent.color = Color.black;
        GuiPresenter.MainManager.GameTime = time*60;
    }

    private void ToggleCoordinateVisibility()
    {
        Debug.Log("toggling coords");
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

        ShowCoordinates = PlayerPrefs.GetInt("ShowCoordinates", 0);

        if (ShowCoordinates == 1)
        {
            ShowCoordinateToggle.isOn = true;
        }
        else
        {
            ShowCoordinateToggle.isOn = false;
        }
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
        if (ShowCoordinateToggle.isOn)
        {
            ShowCoordinates = 1;
        }
        else
        {
            ShowCoordinates = 0;
        }
        PlayerPrefs.SetInt("ShowCoordinates", ShowCoordinates );
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Saves the playerprefs and changes the view
    /// </summary>
    public void GoBack()
    {
        try
        {
            ChangeGameTime();
            ToggleCoordinateVisibility();
            SavePrefs();
            GuiPresenter.EventManager.PostNotification(GameEvents.UpdateSettings, this);
            GameStateManager go = FindObjectOfType<GameStateManager>();

            if (go.CurrentState is PlayingState || go.CurrentState is MultiplayerState)
            {
                Presenter.CloseView(VIEWS.OptionsView);
                GuiPresenter.MainManager.ToggleAllColliders();
                GuiPresenter.MainManager.ToggleCameraActive();
            }
            else
            {
                Presenter.ChangeView(VIEWS.MainmenuView);
            }

            if (SceneSelectDropDown.captionText.text != SceneManager.GetActiveScene().name)
            {
                SceneManager.LoadScene(SceneSelectDropDown.captionText.text);
            }
        }
        catch(FormatException)
        {
            //GameTimeInput.text = "Enter a valid number.";
            GameTimeInput.textComponent.color = Color.red;
        }        
    }
}

