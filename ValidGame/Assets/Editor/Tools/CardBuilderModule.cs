//C# Example
using UnityEditor;
using UnityEngine;

public class CardBuilderEditor : EditorWindow
{
    private string cardTitleStr = "Title";
    private string cardDescriptionStr = "Desc";
    private string matchCodeStr= "A01";
    private bool groupEnabled;
    private bool myBool = true;
    private float scale = 1.0f;
    private Sprite sprite;
   
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
       
        sprite = (Sprite)EditorGUILayout.ObjectField("Sprite", sprite, typeof(Sprite), false);      

        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Card Settings", groupEnabled);
        myBool = EditorGUILayout.Toggle("Enable Drag", myBool);
        scale = EditorGUILayout.Slider("Card Scale", scale, 1, 10);
        EditorGUILayout.EndToggleGroup();

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
        Instantiate(go, Vector3.zero, Quaternion.identity);
    }
}