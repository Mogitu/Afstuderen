using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour {

    public float lookSpeed = 5.0f;
    public float verticalRotation = 0;
    public float horizontalRotation = 0;
    public float verticalRange = 60.0f;
    public float horizontalRange = 90f;
    private bool action;


	// Use this for initialization
	void Start () {
       // Cursor.visible = false;
	
	}
	
	// Update is called once per frame
	void Update () {
        if(action)
        {
            horizontalRotation += Input.GetAxis("Mouse X") * lookSpeed;
            horizontalRotation = Mathf.Clamp(horizontalRotation, -horizontalRange, horizontalRange);
            verticalRotation -= Input.GetAxis("Mouse Y") * lookSpeed;
            verticalRotation = Mathf.Clamp(verticalRotation, -verticalRange, verticalRange);
            Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);
        }
     
       
    }

    void DisableAnimator()
    {
        Animator anim = GetComponent<Animator>();    
        anim.enabled = false;
        horizontalRotation = transform.rotation.y;
        verticalRotation = 26;//transform.rotation.x;
        action = true;
    }
}
