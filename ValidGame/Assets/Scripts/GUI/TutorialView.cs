using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using AMC.GUI;
using UnityEngine.UI;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   .
/// </summary>
public class TutorialView : View
{
    public Text TitleText;
    public Text InfoText;
    public RawImage Image;

    private bool TutorialComplete = false;
    private Dictionary<string, TutorialComponent> TutorialComponents;
    private GuiPresenter GuiPresenter;
    private TutorialModel CurrentTutorial;
    private TutorialModel[] TutorialModels;
    private MovieTexture[] MovieTextures;
    private MovieTexture CurrentMovieTexture;
    private int CurrentTutorialId;

    void Start()
    {
        GuiPresenter = GetPresenterType<GuiPresenter>();         
        InitTutorialData();
    }

    //TODO: Divide in smaller methods
    private void InitTutorialData()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(Environment.CurrentDirectory + "/Tutorials");
        Debug.Log(Environment.CurrentDirectory);
        FileInfo[] filesInfo = directoryInfo.GetFiles("*.tut");
        TutorialModels = new TutorialModel[filesInfo.Length];
        for (int i = 0; i < filesInfo.Length; i++)
        {
            FileInfo tmpInfo = filesInfo[i];
            TutorialModel model = new TutorialModel(tmpInfo.FullName);           
            TutorialModels[i] = model;
        }
        CurrentTutorialId = 0;
        CurrentTutorial = TutorialModels[CurrentTutorialId];
        SetTutorialData(CurrentTutorial);

        MovieTextures = Resources.LoadAll<MovieTexture>("Movies");
        CurrentMovieTexture = MovieTextures[CurrentTutorialId];
        Image.texture = CurrentMovieTexture;
        CurrentMovieTexture.loop = true;
        CurrentMovieTexture.Play();

    }

    private void SetTutorialData(TutorialModel model)
    {
        TitleText.text = model.Title;
        InfoText.text = model.TutorialText;
    }

    private void OnEnable()
    {
        if (CurrentMovieTexture)
            CurrentMovieTexture.Play();
    }

    void Update()
    {
       
    }

    private void OnDisable()
    {
        if (CurrentMovieTexture)
            CurrentMovieTexture.Pause();
    }

    public void NextTutorial()
    {
        CurrentMovieTexture.Stop();
        CurrentTutorialId++;
        if (CurrentTutorialId > TutorialModels.Length - 1)
        {
            CurrentTutorialId = 0;
        }
        CurrentTutorial = TutorialModels[CurrentTutorialId];
        CurrentMovieTexture = MovieTextures[CurrentTutorialId];
        Image.texture = CurrentMovieTexture;
        SetTutorialData(CurrentTutorial);
        CurrentMovieTexture.Play();
    }

    public void PreviousTutorial()
    {
        CurrentMovieTexture.Stop();
        CurrentTutorialId--;
        if (CurrentTutorialId < 0)
        {
            CurrentTutorialId = TutorialModels.Length - 1;
        }
        CurrentTutorial = TutorialModels[CurrentTutorialId];
        CurrentMovieTexture = MovieTextures[CurrentTutorialId];
        Image.texture = CurrentMovieTexture;
        SetTutorialData(CurrentTutorial);
        CurrentMovieTexture.Play();
    }

    public void Close()
    {
        GuiPresenter.ToggleTutorial();
    }   
}