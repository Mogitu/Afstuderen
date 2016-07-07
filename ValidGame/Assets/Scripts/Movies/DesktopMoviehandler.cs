using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class DesktopMoviehandler : IMovieHandler
{
    private TutorialModel CurrentTutorial;
    private TutorialModel[] TutorialModels;  
    private int CurrentTutorialId;
   // private Dictionary<string, MovieTexture> MovieTextures;
   // private MovieTexture CurrentMovieTexture;

    public DesktopMoviehandler()
    {

    }

    public bool Loop
    {
        get
        {
            throw new NotImplementedException();
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public void Pause()
    {
        throw new NotImplementedException();
    }

    public void Play()
    {
        throw new NotImplementedException();
    }

    public void Stop()
    {
        throw new NotImplementedException();
    }

    public void InitTutorialData()
    {
        throw new NotImplementedException();
    }

    public void FillTutorialModels()
    {
        throw new NotImplementedException();
    }

    public void SetTutorialData(int currentTutorialId)
    {
        throw new NotImplementedException();
    }

    public void SetTutorialContext()
    {
        throw new NotImplementedException();
    }

    public void FillMovieTextures()
    {
        throw new NotImplementedException();
    }

    public void OnEnable()
    {
        throw new NotImplementedException();
    }

    public void NextTutorial()
    {
        throw new NotImplementedException();
    }

    public void PreviousTutorial()
    {
        throw new NotImplementedException();
    }

    public void Close()
    {
        throw new NotImplementedException();
    }
}
