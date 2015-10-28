using UnityEngine;
using System.Collections;

public class CameraRotationalMovement : IMovement {
    public float verticalRotation = 0;
    public float horizontalRotation = 0;
    public float lookSpeed = 5.0f;

    public void Move(GameObject gameObject)
    {
        CameraController cont = gameObject.GetComponent<CameraController>();
        //Add mouse axis movement to the rotation
        horizontalRotation += Input.GetAxis("Mouse X") * lookSpeed;
        verticalRotation -= Input.GetAxis("Mouse Y") * lookSpeed;

        //keep rotation within allowed limits
        horizontalRotation = Mathf.Clamp(horizontalRotation, -cont.horizontalRange, cont.horizontalRange);
        verticalRotation = Mathf.Clamp(verticalRotation, -cont.verticalRangeUp, cont.verticalRangeDown);
        //set the final rotation values.
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);
    }
}
