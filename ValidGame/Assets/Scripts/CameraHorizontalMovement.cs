using UnityEngine;
using System.Collections;

public class CameraHorizontalMovement :IMovement {

	public void Move(GameObject gameObject)
    {
        CameraController cont = gameObject.GetComponent<CameraController>();
        Camera.main.transform.Translate(new Vector3(Input.GetAxis("Mouse X") * cont.moveSpeed * Time.deltaTime, Input.GetAxis("Mouse Y") * cont.moveSpeed * Time.deltaTime, 0));
        gameObject.transform.position = new Vector3(Mathf.Clamp(gameObject.transform.position.x, -0.55f, 0.075f), Mathf.Clamp(gameObject.transform.position.y, 0.29f, 0.45f), Mathf.Clamp(gameObject.transform.position.z, -0.55f, -0.1f));
    }
}
