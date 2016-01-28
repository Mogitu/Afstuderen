using UnityEngine.UI;

public class MultiplayerGameplayView : View {
    public InputField inputField;
    public Text chatBoxTxt;

    public void AppendTextToBox()
    {       
        string strA = inputField.text+"NEWLINE";
        string strB = strA.Replace("NEWLINE", "\n");
        chatBoxTxt.text += strB;      
        inputField.text = "";                
    }    
}
