using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour
{
    public float lookSpeed = 5.0f;
    public float moveSpeed = 1.0f;
    public float verticalRange = 60.0f;
    public float horizontalRange = 90f;
    public float verticalRotation = 0;
    public float horizontalRotation = 0;

    // Use this for initialization
    void Start()
    {
        // Cursor.visible = false;	
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1))
        {
            //Add mouse axis movement to the rotation
            horizontalRotation += Input.GetAxis("Mouse X") * lookSpeed;
            verticalRotation -= Input.GetAxis("Mouse Y") * lookSpeed;

            //keep rotation within allowed limits
            horizontalRotation = Mathf.Clamp(horizontalRotation, -horizontalRange, horizontalRange);
            verticalRotation = Mathf.Clamp(verticalRotation, -verticalRange, verticalRange);

            //set the final rotation values.
            Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);

            //moves the camera sideways when moving the mouse
            Camera.main.transform.Translate(Vector3.right * Input.GetAxis("Mouse X") * moveSpeed * Time.deltaTime);
        }       
    }

    void DisableAnimator()
    {
        Animator anim = GetComponent<Animator>();
        anim.enabled = false;
        horizontalRotation = transform.rotation.y;
        verticalRotation = 26;//transform.rotation.x;       
    }
}
