using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :
/// </summary>
public class CardLoader {
    private string Path;
    private List<CardModel> CardModels;	

    public CardLoader(string path)
    {
        Path = path;
        LoadCards();
        InstantiateCards();
    }

    private void LoadCards()
    {
        CardModels = new List<CardModel>();
        DirectoryInfo directoryInfo = new DirectoryInfo(Environment.CurrentDirectory+Path);
        FileInfo[] filesInfo = directoryInfo.GetFiles("*.card");
        foreach(FileInfo file in filesInfo)
        {
            string title = AmcUtilities.ReadFileItem("title", file.FullName);
            string matchCode = AmcUtilities.ReadFileItem("matchcode", file.FullName);
            string description = AmcUtilities.ReadFileItem("description", file.FullName);

            CardModel cardModel = new CardModel(title,matchCode,description);
            CardModels.Add(cardModel);            
        }
    }  
    
    private void InstantiateCards()
    {
        for(int i=0; i<CardModels.Count;i++)
        {
            CardModel model = CardModels[i];
            //create scenecard
            GameObject go = Resources.Load<GameObject>("card");
            Card card = go.GetComponent<Card>();
            card.SetData(model.Title,model.Description,model.MatchCode,TeamType.CheckAndAct);
            go.name = "SceneCard"+card.Title;          
            GameObject newGo = (GameObject)UnityEngine.Object.Instantiate(go, Vector3.zero, go.transform.rotation);

            //create gui card
            GameObject go2 = Resources.Load<GameObject>("guiCard");
            GuiCard guiCard = go2.GetComponent<GuiCard>();
            guiCard.MatchCode = model.MatchCode;
            go2.name = "GUICard" + model.MatchCode;
            GameObject newGo2 = (GameObject)UnityEngine.Object.Instantiate(go2,Vector3.zero, go2.transform.rotation);
        }       
    }  
}
