using UnityEngine;

public class CheatBookScript : MonoBehaviour
{
    public ResultChecker Checker;
    private bool Cheating;
    private float CheatTimer;
    private int MaxCheatTime = 5;

    void OnMouseDown()
    {
        if (!Cheating)
        {
            Cheating = true;
            Checker.CalculateResults();
        }
        else
        {
            Cheating = false;
            Checker.HideResults();
        }
    }

    void Update()
    {
        if (Cheating)
        {
            CheatTimer += Time.deltaTime;
            if (CheatTimer >= MaxCheatTime)
            {
                CheatTimer = 0.0f;
                Cheating = false;
                Checker.HideResults();
            }
        }
    }
}
