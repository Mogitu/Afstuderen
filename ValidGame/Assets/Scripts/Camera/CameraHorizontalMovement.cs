using UnityEngine;
using System.Collections;

namespace VALIDGame
{
    public class CameraDirectionalMovement : IMovement
    {
        //Move camera up and down
        public void Move(CameraController cont)
        {
            Camera.main.transform.Translate(new Vector3(Input.GetAxis("Mouse X") * cont.moveSpeed * Time.deltaTime, Input.GetAxis("Mouse Y") * cont.moveSpeed * Time.deltaTime, 0));
            cont.transform.position = new Vector3(Mathf.Clamp(cont.transform.position.x, -0.55f, 0.075f),
                                                  Mathf.Clamp(cont.transform.position.y, 0.29f, 0.45f),
                                                  Mathf.Clamp(cont.transform.position.z, -0.55f, -0.1f));
        }
    }
}


