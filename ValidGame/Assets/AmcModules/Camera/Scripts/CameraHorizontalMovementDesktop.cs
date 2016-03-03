using UnityEngine;
/// <summary>
/// Author  :   Maikel van Munsteren 
/// Desc    :   Moves camera up and down
/// </summary>

namespace AMC.Camera
{
    public class CameraDirectionalMovementDesktop : ICameraMovement
    {
        //Move camera up and down
        public void Move(ICameraController controller)
        {
            CameraControllerDesktop cont = (CameraControllerDesktop)controller;
            cont.gameObject.transform.Translate(new Vector3(Input.GetAxis("Mouse X") * cont.MoveSpeed * Time.deltaTime, Input.GetAxis("Mouse Y") * cont.MoveSpeed * Time.deltaTime, 0));
            cont.transform.position = new Vector3(Mathf.Clamp(cont.transform.position.x, -0.55f, 0.075f),
                                                  Mathf.Clamp(cont.transform.position.y, 0.29f, 0.45f),
                                                  Mathf.Clamp(cont.transform.position.z, -0.55f, -0.1f));
        }
    }
}
