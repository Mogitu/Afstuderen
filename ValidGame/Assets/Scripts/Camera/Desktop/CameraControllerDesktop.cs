using UnityEngine;
using AMC.Camera;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Handles common camera functions.
/// TODO    :   Make this abstract.
/// </summary>
public class CameraControllerDesktop : CameraController
{
    public float LookSpeed = 5.0f;
    public float MoveSpeed = 1.0f;
    public float ZoomSpeed = 2.0f;
    public float VerticalRangeUp = 15f;
    public float VerticalRangeDown = 40f;
    public float HorizontalRange = 90f;
    public float VerticalRotation = 0;
    public float HorizontalRotation = 0;
    public float MaxDistanceHorizontal = 0.3f;
    public float MaxVerticalDistance = 0.15f;    

    // Use this for initialization
    void Start()
    {
        AddMovementPattern("Horizontal", new CameraDirectionalMovementDesktop());
        AddMovementPattern("Rotational", new CameraRotationalMovementDesktop());
        AddMovementPattern("Zoom", new CameraZoomMovementDesktop());
    }    

    public override void HandleInput()
    {
        //move horizontally and vertically 
        if ((Input.GetMouseButton(0) && Input.GetMouseButton(1)) || Input.GetMouseButton(2))
        {
            SetCameraMovement("Horizontal");
            return;
        }

        //rotate the camera
        if (Input.GetMouseButton(1))
        {
            SetCameraMovement("Rotational");
            return;
        }

        //Zoom camera towards front
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            SetCameraMovement("Zoom");
        }
    }   
}