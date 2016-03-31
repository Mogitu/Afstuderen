using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

    public float Speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.Rotate(Vector3.forward * Speed);
	}
}