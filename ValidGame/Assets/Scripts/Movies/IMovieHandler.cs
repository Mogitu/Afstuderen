using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

interface IMovieHandler
{
    // void Stop();
    //void Play();
    //void Pause();
    //bool Loop { get; set; }
    void InitTutorialData();
    void FillTutorialModels();
    void SetTutorialData(int currentTutorialId);
    void SetTutorialContext();
    void FillMovieTextures();
    void OnEnable();
    void NextTutorial();
    void PreviousTutorial();
    void Close();
}

