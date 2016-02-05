using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Handles common camera functions.
/// TODO    :   Make this abstract.
/// </summary>
public class CameraControllerDesktop : MonoBehaviour, ICameraController
{
    public float lookSpeed = 5.0f;
    public float moveSpeed = 1.0f;
    public float zoomSpeed = 2.0f;
    public float verticalRangeUp = 15f;
    public float verticalRangeDown = 40f;
    public float horizontalRange = 90f;
    public float verticalRotation = 0;
    public float horizontalRotation = 0;
    public float maxDistanceHorizontal = 0.3f;
    public float maxVerticalDistance = 0.15f;

    private Dictionary<string, ICameraMovement> movementSet;
    private ICameraMovement activeMovement;

    // Use this for initialization
    void Start()
    {
        movementSet = new Dictionary<string, ICameraMovement>(){
            {"Horizontal", new CameraDirectionalMovement()},
            {"Rotational", new CameraRotationalMovement()},
            {"Zoom", new CameraZoomMovement()}};
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    public void SetCameraMovement(ICameraMovement movement)
    {
        activeMovement = movement;
        activeMovement.Move(this);
    }

    //Played at the start of the game. Moves camera to gameboard and hands camera control over to the player. See the animation statemachine.
    public void RunGameStartAnimation()
    {
        Camera.main.GetComponent<Animator>().SetBool("GameStarted", true);
    }

    public void RunGameEndAnimation()
    {
        Camera.main.GetComponent<CameraControllerDesktop>().enabled = false;
        Camera.main.GetComponent<Animator>().enabled = true;
        Camera.main.GetComponent<Animator>().SetBool("GameOver", true);
    }

    public void HandleInput()
    {
        //move horizontally and vertically 
        if ((Input.GetMouseButton(0) && Input.GetMouseButton(1)) || Input.GetMouseButton(2))
        {
            SetCameraMovement(movementSet["Horizontal"]);
            return;
        }

        //rotate the camera
        if (Input.GetMouseButton(1))
        {
            SetCameraMovement(movementSet["Rotational"]);
            return;
        }

        //Zoom camera towards front
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            SetCameraMovement(movementSet["Zoom"]);
        }
    }
}