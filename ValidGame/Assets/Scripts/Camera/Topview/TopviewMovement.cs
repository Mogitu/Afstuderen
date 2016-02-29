using UnityEngine;
using AMC.Camera;

public class TopviewMovement : ICameraMovement
{
    public void Move(ICameraController cont)
    {
        Camera.main.transform.Translate(Vector3.right*2*Time.deltaTime);
    }
}