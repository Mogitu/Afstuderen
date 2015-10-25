using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public float lookSpeed = 5.0f;
    public float moveSpeed = 1.0f;
    public float verticalRangeUp = 15f;
    public float verticalRangeDown = 40f;
    public float horizontalRange = 90f;
    public float verticalRotation = 0;
    public float horizontalRotation = 0;
    public float maxDistanceHorizontal = 0.3f;
    public float maxVerticalDistance = 0.15f;

    private Vector3 originalPos;

    // Use this for initialization
    void Start()
    {
        //Cursor.visible = false;	
    }

    // Update is called once per frame
    void Update()
    {
        //move horizontally and vertically 
        if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
        {
            /*
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");

            // moves the camera sideways when moving the mouse
            Vector3 movement = new Vector3(x, 0, 0) * Time.deltaTime;
            Vector3 newPos = transform.position + movement;
            Vector3 offSet = newPos - originalPos;
            transform.position = originalPos + Vector3.ClampMagnitude(offSet, maxDistanceHorizontal);           

            // moves the camera sideways when moving the mouse
            Vector3 movementy = new Vector3(0, y, 0) * Time.deltaTime;
            Vector3 newPosy = transform.position + movementy;
            Vector3 offSety = newPosy - originalPos;
            transform.position = originalPos + Vector3.ClampMagnitude(offSety, maxVerticalDistance);
            */

            Camera.main.transform.Translate(new Vector3(1, 0, 0) * Input.GetAxis("Mouse X") * moveSpeed * Time.deltaTime);
            Camera.main.transform.Translate(new Vector3(0, 1, 0) * Input.GetAxis("Mouse Y") * moveSpeed * Time.deltaTime);
            return;
        }

        //rotate the camera
        if (Input.GetMouseButton(1))
        {
            //Add mouse axis movement to the rotation
            horizontalRotation += Input.GetAxis("Mouse X") * lookSpeed;
            verticalRotation -= Input.GetAxis("Mouse Y") * lookSpeed;

            //keep rotation within allowed limits
            horizontalRotation = Mathf.Clamp(horizontalRotation, -horizontalRange, horizontalRange);
            verticalRotation = Mathf.Clamp(verticalRotation, -verticalRangeUp, verticalRangeDown);

            //set the final rotation values.
            Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);
        }

        //move camera forward or backward
        float wheelMovement = Input.GetAxis("Mouse ScrollWheel");
        if (wheelMovement > 0)
        {
            Camera.main.transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
        }
        else if (wheelMovement < 0)
        {
            Camera.main.transform.Translate(-Vector3.forward * Time.deltaTime * moveSpeed);
        }
    }

    void DisableAnimator()
    {
        Animator anim = GetComponent<Animator>();
        anim.enabled = false;
        horizontalRotation = transform.rotation.y;
        verticalRotation = 26;//transform.rotation.x;            
        originalPos = transform.position;
    }
}
