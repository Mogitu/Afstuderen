using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Tool to help develop valid gamecards.
/// TODO    :   SOLID!
/// </summary>
public class CardBuilderEditor : EditorWindow
{
    private string cardTitleStr = "Title";
    private string cardDescriptionStr = "Placeholder description";
    private string matchCodeStr = "1a";
    private Sprite sprite;
    private Sprite guiSprite;

    [MenuItem("AMC Centre/Tools/VALID/Card builder")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(CardBuilderEditor), false, "Card builder");
    }

    void OnGUI()
    {
        GUILayout.Label("Basic Card Settings", EditorStyles.boldLabel);
        cardTitleStr = EditorGUILayout.TextField("Card title", cardTitleStr);

        GUILayout.Label("Description", EditorStyles.label);
        cardDescriptionStr = EditorGUILayout.TextArea(cardDescriptionStr, GUILayout.Height(100));
        matchCodeStr = EditorGUILayout.TextField("Card Matchcode", matchCodeStr);

        sprite = (Sprite)EditorGUILayout.ObjectField("Scenery sprite", sprite, typeof(Sprite), false);
        guiSprite = (Sprite)EditorGUILayout.ObjectField("Browser sprite", guiSprite, typeof(Sprite), false);

        /*
        EditorGUILayout.BeginVertical();
        EditorGUI.DrawPreviewTexture(new Rect(new Vector2(0,300), new Vector2(800, 800)),AssetPreview.GetAssetPreview(PreviewObject().gameObject),PreviewObject().GetComponent<Renderer>().sharedMaterial);
        EditorGUILayout.EndVertical();            
        
        GUILayout.Label("Building the card places 2 objects below the cards object in the scene, \n if this object does not exist it will be created.",
                        EditorStyles.label);
         
        */
        if (GUILayout.Button("Build Card"))
        {
            CreateCard();
        }
    }

    private Card PreviewObject()
    {
        GameObject parent = GameObject.Find("Cards");
        if (!parent)
        {
            parent = new GameObject();
            parent.name = "Cards";
        }
        GameObject go = Resources.Load<GameObject>("card");
        Card card = go.GetComponent<Card>();
        card.SetData(cardTitleStr, cardDescriptionStr, matchCodeStr);
        SpriteRenderer spr = card.Sprite.GetComponent<SpriteRenderer>();
        spr.sprite = this.sprite;
        go.transform.rotation = Quaternion.identity;
        return card;
    }

    void CreateCard()
    {
        GameObject parent = GameObject.Find("Cards");
        if (!parent)
        {
            parent = new GameObject();
            parent.name = "Cards";
        }
        GameObject go = Resources.Load<GameObject>("card");
        Card card = go.GetComponent<Card>();
        card.SetData(cardTitleStr, cardDescriptionStr, matchCodeStr);
        SpriteRenderer spr = card.Sprite.GetComponent<SpriteRenderer>();
        spr.sprite = this.sprite;

        GameObject go2 = Resources.Load<GameObject>("guiCard");
        GuiCard guiCard = go2.GetComponent<GuiCard>();
        guiCard.MatchCode = this.matchCodeStr;
        Image spr2 = guiCard.GetComponent<Image>();
        spr2.sprite = guiSprite;
        go.name = "SceneCard" + matchCodeStr;
        go2.name = "GUICard" + matchCodeStr;

        GameObject newGo2 = (GameObject)Instantiate(go2, Vector3.zero, go2.transform.rotation);
        newGo2.transform.SetParent(parent.transform, false);
        GameObject newGo = (GameObject)Instantiate(go, Vector3.zero, go.transform.rotation);
        newGo.transform.SetParent(parent.transform, false);
    }
}

