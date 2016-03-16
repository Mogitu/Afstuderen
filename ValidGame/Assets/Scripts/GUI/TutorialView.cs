using System.Collections.Generic;
using AMC.GUI;
using UnityEngine.UI;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   .
/// </summary>
public class TutorialView : View {        

    public Text TitleText;
    public Text InfoText;

    private bool TutorialComplete = false;
    private bool ShowTutorial;
    private Dictionary<string, TutorialComponent> TutorialComponents;  
    private GuiPresenter GuiPresenter;

    void Start()
    {
        GuiPresenter = GetPresenterType<GuiPresenter>();
        CreateTutorialDictionary(out TutorialComponents);
        //DisableAllAttachedComponents();
        if(GuiPresenter.ShowTutorial)
        {
            ShowTutorial = true;                 
        }
        InitTutorialData();
    }	


    private void InitTutorialData()
    {
        TitleText.text = AmcUtilities.ReadFileItem("title","Tutorial.tut");
        InfoText.text = AmcUtilities.ReadFileItem("text", "Tutorial.tut");
    }

    public void Close()
    {
        GuiPresenter.ToggleTutorial();
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
}
