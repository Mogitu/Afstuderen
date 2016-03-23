using UnityEngine;
using System.Collections;

public class ArrowScript : MonoBehaviour {
    public Transform TextTransform;
    public GameObject ArrowObject;
    public GameObject DropObject;
    public GameObject GrabObject;
    public float MaxUpDown = 1.0f;
    public float Speed = 1.0f;
    private float Angle =0;
    private float OriginalYPos;
    private float OriginalYText;

    void Start()
    {
        OriginalYPos = transform.position.y;
        OriginalYText = TextTransform.position.y;     
    }   

    void OnDisable()
    {
        transform.gameObject.SetActive(false);
    }	

    void FixedUpdate()
    {
        Angle += Speed;
        if(Angle>360)
        {
            Angle = -360;
        }
        Vector3 newPos = transform.position;
        newPos.y = MaxUpDown * Mathf.Sin(Angle* (Mathf.PI/180))+OriginalYPos;
        transform.position = newPos;

        Vector3 newPosText = TextTransform.position;
        newPosText.y = MaxUpDown * Mathf.Sin(Angle * (Mathf.PI / 180)) +OriginalYText;
        TextTransform.position = newPosText;
    }

    public void RotateDown()
    {
        DropObject.SetActive(true);
        GrabObject.SetActive(false);
        ArrowObject.transform.rotation = Quaternion.Euler(-90,0,0);
    }

    public void RotateUp()
    {
        DropObject.SetActive(false);
        GrabObject.SetActive(true);
        ArrowObject.transform.rotation = Quaternion.Euler(90, 0, 0);
    }
}
