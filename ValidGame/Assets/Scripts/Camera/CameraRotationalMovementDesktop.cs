using UnityEngine;
using AMC.Camera;
/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Rotates camera
/// </summary>
public class CameraRotationalMovementDesktop : ICameraMovement
{
    public void Move(ICameraController controller)
    {
        CameraControllerDesktop cont = (CameraControllerDesktop)controller;
        //Add mouse axis movement to the rotation.
        cont.horizontalRotation += Input.GetAxis("Mouse X") * cont.lookSpeed;
        cont.verticalRotation -= Input.GetAxis("Mouse Y") * cont.lookSpeed;

        //keep rotation within allowed limits.
        cont.horizontalRotation = Mathf.Clamp(cont.horizontalRotation, -cont.horizontalRange, cont.horizontalRange);
        cont.verticalRotation = Mathf.Clamp(cont.verticalRotation, -cont.verticalRangeUp, cont.verticalRangeDown);
        //set the final rotation values.
        Camera.main.transform.localRotation = Quaternion.Euler(cont.verticalRotation, cont.horizontalRotation, cont.gameObject.transform.rotation.z);
    }
}