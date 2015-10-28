using UnityEngine;


public class CameraZoomMovement : IMovement
{
    public void Move(GameObject gameObject)
    {
        CameraController cont = gameObject.GetComponent<CameraController>();
        Camera.main.transform.Translate(Input.GetAxis("Mouse ScrollWheel") * Vector3.forward * Time.deltaTime * cont.zoomSpeed);
        gameObject.transform.position = new Vector3(Mathf.Clamp(gameObject.transform.position.x, -0.55f, 0.075f), Mathf.Clamp(gameObject.transform.position.y, 0.29f, 0.45f), Mathf.Clamp(gameObject.transform.position.z, -0.55f, -0.1f));
    }
}

