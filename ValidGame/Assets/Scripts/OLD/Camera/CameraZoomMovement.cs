using UnityEngine;


    public class CameraZoomMovement : IMovement
    {
        public void Move(CameraController cont)
        {
            //TODO: replace hardcoded values for inspector properties.
            Camera.main.transform.Translate(Input.GetAxis("Mouse ScrollWheel") * Vector3.forward * Time.deltaTime * cont.zoomSpeed);
            cont.transform.position = new Vector3(Mathf.Clamp(cont.transform.position.x, -0.55f, 0.075f),
                                                  Mathf.Clamp(cont.transform.position.y, 0.29f, 0.45f),
                                                  Mathf.Clamp(cont.transform.position.z, -0.55f, -0.1f));
        }
    }



