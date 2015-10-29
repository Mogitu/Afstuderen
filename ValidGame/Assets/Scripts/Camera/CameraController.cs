using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
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
    
    private Dictionary<string, IMovement> movementSet;
    private IMovement activeMovement;

    // Use this for initialization
    void Start()
    {
        //Cursor.visible = false;	
        movementSet = new Dictionary<string,IMovement>(){
            {"Horizontal", new CameraDirectionalMovement()},
            {"Rotational", new CameraRotationalMovement()},
            {"Zoom", new CameraZoomMovement()}
        };         
    }

    // Update is called once per frame
    void Update()
    {
        //move horizontally and vertically 
        if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
        {
            SetMovement(movementSet["Horizontal"]);           
            return; 
        }

        //rotate the camera
        if (Input.GetMouseButton(1))
        {
            SetMovement(movementSet["Rotational"]);            
            return;
        }

        //Zoom camera towards front
        if(Input.GetAxis("Mouse ScrollWheel")!=0)
        {
            SetMovement(movementSet["Zoom"]);            
        }
    }

    void SetMovement(IMovement movement)
    {
        activeMovement = movement;
        activeMovement.Move(this);
    }

    void DisableAnimator()
    {
        Animator anim = GetComponent<Animator>();
        anim.enabled = false;
        horizontalRotation = transform.rotation.y;
        verticalRotation = 26;//transform.rotation.x;              
    }
}
