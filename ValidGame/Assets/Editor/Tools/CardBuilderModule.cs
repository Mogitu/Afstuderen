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
    private float scale = 1.23f;
   
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
        
        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Card Settings", groupEnabled);
        myBool = EditorGUILayout.Toggle("Enable Drag", myBool);
        scale = EditorGUILayout.Slider("Card Scale", scale, -3, 3);
        EditorGUILayout.EndToggleGroup();

        if (GUILayout.Button("Build Card"))
        {
            CreateCard();
        }
    }

    void CreateCard()
    {
        GameObject go = Resources.Load<GameObject>("Tools/card");
        go.transform.localScale *= scale;
        Card card = go.GetComponent<Card>();
        card.SetData(cardTitleStr, cardDescriptionStr, matchCodeStr);
        Instantiate(go, Vector3.zero, Quaternion.identity);
    }
}