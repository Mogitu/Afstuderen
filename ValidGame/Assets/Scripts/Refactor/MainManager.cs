using UnityEngine;
using System.Collections;

public class MainManager : MonoBehaviour {

    void Awake()
    {
        
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartMultiplayer(){
        
    }

    public void StartPracticeRound()
    {
        Debug.Log("Start practice");
    }

    public void QuitApplication()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
}
