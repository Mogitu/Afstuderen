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
        cont.HorizontalRotation += Input.GetAxis("Mouse X") * cont.LookSpeed;
        cont.VerticalRotation -= Input.GetAxis("Mouse Y") * cont.LookSpeed;

        //keep rotation within allowed limits.
        cont.HorizontalRotation = Mathf.Clamp(cont.HorizontalRotation, -cont.HorizontalRange, cont.HorizontalRange);
        cont.VerticalRotation = Mathf.Clamp(cont.VerticalRotation, -cont.VerticalRangeUp, cont.VerticalRangeDown);
        //set the final rotation values.
        Camera.main.transform.localRotation = Quaternion.Euler(cont.VerticalRotation, cont.HorizontalRotation, cont.gameObject.transform.rotation.z);
    }
}