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
}
