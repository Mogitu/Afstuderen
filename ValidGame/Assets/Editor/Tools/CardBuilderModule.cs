//C# Example
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace AMCTools
{
    public class CardBuilderEditor : EditorWindow
    {
        private string cardTitleStr = "Title";
        private string cardDescriptionStr = "Desc";
        private string matchCodeStr = "A01";
        private bool groupEnabled;
        private string path = "";

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
            guiSprite = (Sprite)EditorGUILayout.ObjectField("Browser sprite", guiSprite, typeof(Sprite), false);

            groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Card Settings", groupEnabled);
            if (GUILayout.Button("Save"))
            {
                path = EditorUtility.SaveFolderPanel("Save textures to directory", "", "");
            }
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
            GameObject parent = GameObject.Find("Cards");            
            if(!parent)
            {
                parent = new GameObject();
                parent.name = "Cards";
            }          
            Debug.Log(path);
            GameObject go = Resources.Load<GameObject>("Tools/card");
            //go.transform.localScale *= scale;
            CardEdit card = go.GetComponent<CardEdit>();
            card.SetData(cardTitleStr, cardDescriptionStr, matchCodeStr);
            SpriteRenderer spr = card.sprite.GetComponent<SpriteRenderer>();
            spr.sprite = this.sprite;

            GameObject go2 = Resources.Load<GameObject>("Tools/guiCard");
            GuiCard guiCard = go2.GetComponent<GuiCard>();
            guiCard.matchCode = this.matchCodeStr;
            Image spr2 = guiCard.GetComponent<Image>();
            spr2.sprite = guiSprite;

            if (path.Length != 0)
            {
                go.name = "SceneCard" + matchCodeStr;
                PrefabUtility.CreatePrefab("Assets/" + go.name + ".prefab", go);

                go2.name = "GUICard" + matchCodeStr;
                PrefabUtility.CreatePrefab(path + go2.name + ".prefab", go2);
            }
            else
            {
                GameObject newGo2 = (GameObject)Instantiate(go2, Vector3.zero, go2.transform.rotation);
                newGo2.transform.parent = parent.transform;
                GameObject newGo = (GameObject)Instantiate(go, Vector3.zero, go.transform.rotation);
                newGo.transform.parent = parent.transform;
            }
        }
    }
}
