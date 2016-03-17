using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using AMC.GUI;
using UnityEngine.UI;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   .
/// TODO    :   To much responsibilities; loading of videos could be a much better task for a dedicated object/class.
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
    private Dictionary<string, MovieTexture> MovieTextures;
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
        FillTutorialModels();
        FillMovieTextures();
        SetTutorialData(CurrentTutorialId);
    }

    private void FillTutorialModels()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(Environment.CurrentDirectory + "/Tutorials");
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
    }

    private void FillMovieTextures()
    {
        MovieTexture[] MovieTexturesTmp = Resources.LoadAll<MovieTexture>("Movies");
        MovieTextures = new Dictionary<string, MovieTexture>();
        foreach (MovieTexture tex in MovieTexturesTmp)
        {
            MovieTextures.Add(tex.name, tex);
        }
    }

    private void SetTutorialData(int id)
    {
        CurrentTutorial = TutorialModels[id];
        if (CurrentMovieTexture != null)
        {
            CurrentMovieTexture.Stop();
        }
        TitleText.text = CurrentTutorial.Title;
        InfoText.text = CurrentTutorial.TutorialText;
        CurrentMovieTexture = MovieTextures[CurrentTutorial.VideoPath];
        Image.texture = CurrentMovieTexture;
        CurrentMovieTexture.Play();
    }

    private void OnEnable()
    {
        if (CurrentMovieTexture)
            CurrentMovieTexture.Play();
    }

    private void OnDisable()
    {
        if (CurrentMovieTexture)
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
    }

    public void PreviousTutorial()
    {
        CurrentTutorialId--;
        if (CurrentTutorialId < 0)
        {
            CurrentTutorialId = TutorialModels.Length - 1;
        }
        SetTutorialData(CurrentTutorialId);
        CurrentMovieTexture.Play();
    }

    public void Close()
    {
        GuiPresenter.ToggleTutorial();
    }
}