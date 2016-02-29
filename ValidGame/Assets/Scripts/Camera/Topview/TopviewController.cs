using UnityEngine;
using AMC.Camera;

public class TopviewController : CameraController {

    public float speed = 2.0f;
    // Use this for initialization
    void Start()
    {
        AddMovementPattern("TopviewMovement", new TopviewMovement());
    }   

    public override void HandleInput()
    {
        if(Input.GetAxis("Mouse X") != 0)
        {           
            SetCameraMovement("TopviewMovement");
        }
    }  
}