//C# Example
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CardBuilderEditor : EditorWindow
{
    private string cardTitleStr = "Title";
    private string cardDescriptionStr = "Desc";
    private string matchCodeStr= "A01";
    private bool groupEnabled;    
    private float scale = 1.0f;
    private Sprite sprite;
    private Sprite guiSprite;
   
    [MenuItem("Valid/Card builder")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(CardBuilderEditor));       
    }

    void OnGUI()
    {
        GUILayout.Label("Basic Card Settings", EditorStyles.boldLabel);
        cardTitleStr = EditorGUILayout.TextField("Card title", cardTitleStr);
        cardDescriptionStr = EditorGUILayout.TextField("Card Description", cardDescriptionStr);
        matchCodeStr = EditorGUILayout.TextField("Card Matchcode", matchCodeStr);
       
        sprite = (Sprite)EditorGUILayout.ObjectField("Scenery sprite", sprite, typeof(Sprite), false);
        guiSprite = (Sprite)EditorGUILayout.ObjectField("Browser sprite", guiSprite, typeof(Sprite),false);

        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Card Settings", groupEnabled);   
        scale = EditorGUILayout.Slider("Card Scale", scale, 1, 10);
        EditorGUILayout.EndToggleGroup();

        GUILayout.Label("Building the card creates two objects in the resources folder.", 
                        EditorStyles.label);
        if (GUILayout.Button("Build Card"))
        {
            CreateCard();
        }
    }

    void CreateCard()
    {
        GameObject go = Resources.Load<GameObject>("Tools/card");
        //go.transform.localScale *= scale;
        CardEdit card = go.GetComponent<CardEdit>();
        card.SetData(cardTitleStr, cardDescriptionStr, matchCodeStr);
        SpriteRenderer spr = card.sprite.GetComponent<SpriteRenderer>();
        spr.sprite = this.sprite;
      
        //GameObject newGo = (GameObject)Instantiate(go, Vector3.zero, go.transform.rotation);
        go.name = "SceneCard" + matchCodeStr;

        PrefabUtility.CreatePrefab("Assets/Resources/GameCards/new/Scene/" + go.name + ".prefab", go);

        GameObject go2 = Resources.Load<GameObject>("Tools/guiCard");
        GuiCard guiCard = go2.GetComponent<GuiCard>();
        guiCard.matchCode = this.matchCodeStr;
        Image spr2 = guiCard.GetComponent<Image>();
        spr2.sprite = guiSprite;
     

        //GameObject newGo2 = (GameObject)Instantiate(go2, Vector3.zero, go2.transform.rotation);
        go2.name = "GUICard" + matchCodeStr;
        PrefabUtility.CreatePrefab("Assets/Resources/GameCards/new/GUI/"+go2.name+".prefab", go2);
       
    }
}