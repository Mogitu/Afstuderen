﻿using UnityEngine;
using System.Collections.Generic;
using AMC.GUI;

public class TutorialView : View {
        
    private bool TutorialComplete = false;
    private bool ShowTutorial;
    private Dictionary<string, TutorialComponent> TutorialComponents;  

    void Start()
    {
        CreateTutorialDictionary(out TutorialComponents);
        DisableAllAttachedComponents();
        if(GetPresenterType<GuiPresenter>().ShowTutorial)
        {
            ShowTutorial = true;
            ShowViewComponent("TutOne");           
        }
    }	

    private void CreateTutorialDictionary(out Dictionary<string,TutorialComponent> dic)
    {
        TutorialComponent[] components = GetComponentsInChildren<TutorialComponent>();
        dic = new Dictionary<string, TutorialComponent>();

        for (int i = 0; i < components.Length; i++)
        {
            dic.Add(components[i].name, components[i]);
        }
    }

    private void DisableAllAttachedComponents()
    {
        TutorialComponent[] components = GetComponentsInChildren<TutorialComponent>();
        for(int i=0; i< components.Length; i++)
        {
            components[i].gameObject.SetActive(false);
        }
    }

    public void ShowBrowserTutorial()
    {
        if (!ShowTutorial)
            return;      
        TutorialComponents["TutOne"].Down();
        ShowViewComponent("Jan");
    }	
}
