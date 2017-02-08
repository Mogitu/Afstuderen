using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using AMC.GUI;
using UnityEngine.UI;


/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   .
/// TODO    :   *To much responsibilities; loading of videos could be a much better task for a dedicated object/class.
///             *Delegate platform specific video loading to single objects.
///             *Improve WebGLMovietexture integration(currently this "barely" works), likely needs changes in the plugin source code.
///             *Preprocessor directives should not be used in to much methods, instead use a single clause that handles/calls the required methods for the platform.
/// </summary>
public class TutorialView : View
{
#if UNITY_STANDALONE_WIN
    public Text TitleText;
    public Text InfoText;
    public Text TutorialCountText;
    public RawImage Image;
    private GuiPresenter GuiPresenter;
    private TutorialModel CurrentTutorial;
    private TutorialModel[] TutorialModels;
    private int CurrentTutorialId;
    private Dictionary<string, MovieTexture> MovieTextures;
    private MovieTexture CurrentMovieTexture;


    void Start()
    {
        GuiPresenter = GetPresenterType<GuiPresenter>();
        InitTutorialData();
    }

    //TODO: Divide in smaller methods
    private void InitTutorialData()
    {
        FillTutorialModels();
        FillMovieTextures();
        SetTutorialData(CurrentTutorialId);
    }

    //Populate the tutorialmodels field
    //TODO: refactor to read from a script rather than external files
    private void FillTutorialModels()
    {
        //Setup the directory information containing the files
        //TODO  :   This should not read files from a folder in the future but instead read from a prefab file/script/object
        DirectoryInfo directoryInfo = new DirectoryInfo(Environment.CurrentDirectory + "/Tutorials");
        //create a new, temporary, array to contain fileinformation. Tutorials have the .tut file extension
        FileInfo[] filesInfo = directoryInfo.GetFiles("*.tut");
        TutorialModels = new TutorialModel[filesInfo.Length];
        //add all tutorialfiles to the tutorialarray
        for (int i = 0; i < filesInfo.Length; i++)
        {
            FileInfo tmpInfo = filesInfo[i];
            TutorialModel model = new TutorialModel(tmpInfo.FullName);
            TutorialModels[i] = model;
        }
        //begin with the first tutorial
        CurrentTutorialId = 0;
        CurrentTutorial = TutorialModels[CurrentTutorialId];
        SetTutorialCountText();
    }

    private void SetTutorialCountText()
    {
        TutorialCountText.text = (CurrentTutorialId + 1) + "/" + TutorialModels.Length;
    }

    //Fill the movie textures dictionary
    private void FillMovieTextures()
    {
        //Load all movies located in the resources\movies folder

        MovieTexture[] MovieTexturesTmp = Resources.LoadAll<MovieTexture>("Movies");
        //initialize the movietextures dictionary
        MovieTextures = new Dictionary<string, MovieTexture>();
        //copy the temporary array into the dictionary
        foreach (MovieTexture tex in MovieTexturesTmp)
        {
            MovieTextures.Add(tex.name, tex);
        }

    }

    //Set up all visible view fields with the current tutorial data
    private void SetTutorialData(int id)
    {
        CurrentTutorial = TutorialModels[id];
        if (CurrentMovieTexture != null)
        {
            //CurrentMovieTexture.Stop();
            CurrentMovieTexture.Pause();
        }
        TitleText.text = CurrentTutorial.Title;
        InfoText.text = CurrentTutorial.TutorialText;
        CurrentMovieTexture = MovieTextures[CurrentTutorial.VideoPath];
        Image.texture = CurrentMovieTexture;
        CurrentMovieTexture.loop = true;
        CurrentMovieTexture.Play();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        //Start the movie only if it is available
        if (CurrentMovieTexture != null)
        {
            CurrentMovieTexture.loop = true;
            CurrentMovieTexture.Play();
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        //pauzes the movie only if available
        if (CurrentMovieTexture != null)
            CurrentMovieTexture.Pause();
    }

    public void NextTutorial()
    {
        CurrentTutorialId++;
        if (CurrentTutorialId > TutorialModels.Length - 1)
        {
            CurrentTutorialId = 0;
        }
        SetTutorialData(CurrentTutorialId);
        SetTutorialCountText();
    }

    public void PreviousTutorial()
    {
        CurrentTutorialId--;
        if (CurrentTutorialId < 0)
        {
            CurrentTutorialId = TutorialModels.Length - 1;
        }
        SetTutorialData(CurrentTutorialId);
        CurrentMovieTexture.loop = true;
        CurrentMovieTexture.Play();
        SetTutorialCountText();
    }

    public void Close()
    {
        GuiPresenter.ToggleTutorial();
    }

    public void OpenHelpInPdf()
    {
        GuiPresenter.OpenHelpPdf();
    }


#else
    public Text TitleText;
    public Text InfoText;
    public Text TutorialCountText;
    public RawImage Image;
    private GuiPresenter GuiPresenter;
    private TutorialModel CurrentTutorial;
    private TutorialModel[] TutorialModels;
    private int CurrentTutorialId;
    //private Dictionary<string, MovieTexture> MovieTextures;
   // private MovieTexture CurrentMovieTexture;


    void Start()
    {
        GuiPresenter = GetPresenterType<GuiPresenter>();
        InitTutorialData();
        GetViewComponent("PrevButton").SetActive(false);
        GetViewComponent("NextButton").SetActive(false);
        GetViewComponent("TutorialCountTxt").SetActive(false);
        GetViewComponent("TutorialText").SetActive(false);
        GetViewComponent("TutorialOne").SetActive(false);  
        GetViewComponent("TutorialWebGL").SetActive(true); 
    }

    //TODO: Divide in smaller methods
    private void InitTutorialData()
    {
        FillTutorialModels();
        FillMovieTextures();
        SetTutorialData(CurrentTutorialId);
    }

    //Populate the tutorialmodels field
    //TODO: refactor to read from a script rather than external files
    private void FillTutorialModels()
    {
     
    }

    private void SetTutorialCountText()
    {
        
    }

    //Fill the movie textures dictionary
    private void FillMovieTextures()
    {
       

    }

    //Set up all visible view fields with the current tutorial data
    private void SetTutorialData(int id)
    {
       
    }

    private void OnEnable()
    {
       
    }

    private void OnDisable()
    {
       
    }

    public void NextTutorial()
    {
       
    }

    public void PreviousTutorial()
    {
        
    }

    public void Close()
    {
        GuiPresenter.ToggleTutorial();
    }

    public void OpenHelpInPdf()
    {
        GuiPresenter.OpenHelpPdf();
    }
#endif

}