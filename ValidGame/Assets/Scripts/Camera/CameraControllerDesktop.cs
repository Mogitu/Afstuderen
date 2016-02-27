using UnityEngine;
using System.Collections.Generic;
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

    private Dictionary<string, ICameraMovement> MovementSet;   

    // Use this for initialization
    void Start()
    {
        MovementSet = new Dictionary<string, ICameraMovement>(){
            {"Horizontal", new CameraDirectionalMovementDesktop()},
            {"Rotational", new CameraRotationalMovementDesktop()},
            {"Zoom", new CameraZoomMovementDesktop()}};
    }
  

    //Played at the start of the game. Moves camera to gameboard and hands camera control over to the player. See the animation statemachine.
    public void RunGameStartAnimation()
    {
        Camera.main.GetComponent<Animator>().SetBool("GameStarted", true);
    }

    public void RunGameEndAnimation()
    {
        Camera.main.GetComponent<CameraController>().enabled =false;
        Camera.main.GetComponent<Animator>().enabled = true;
        Camera.main.GetComponent<Animator>().SetBool("GameOver", true);
    }

    public override void HandleInput()
    {
        //move horizontally and vertically 
        if ((Input.GetMouseButton(0) && Input.GetMouseButton(1)) || Input.GetMouseButton(2))
        {
            SetCameraMovement(MovementSet["Horizontal"]);
            return;
        }

        //rotate the camera
        if (Input.GetMouseButton(1))
        {
            SetCameraMovement(MovementSet["Rotational"]);
            return;
        }

        //Zoom camera towards front
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            SetCameraMovement(MovementSet["Zoom"]);
        }
    }   
}