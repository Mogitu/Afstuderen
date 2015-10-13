using UnityEngine;
using System.Collections;

public class FaceCamera : MonoBehaviour {

    // Use this for initialization
    private Transform cameraTransform;
	void Start () {
        cameraTransform= Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 cameraPosition = cameraTransform.position;

        transform.LookAt(Camera.main.transform.position);
	}
}
