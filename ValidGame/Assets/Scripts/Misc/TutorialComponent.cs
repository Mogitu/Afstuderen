using UnityEngine;
using System.Collections;

public class TutorialComponent : MonoBehaviour
{
    public float ScaleFactor = 1;
    public float MaxScale = 1.5f;
    public float NormalScale = 1.0f;
    public float Minscale = 0.5f;

    // Use this for initialization
    void Start()
    {
        transform.localScale = new Vector3(1,1,1);        
    }   

    void OnEnable()
    {
       StartCoroutine("IncreaseScale");
    }

    void OnDisable()
    {
        transform.localScale = new Vector3(0, 0, 0);
    }

    public void Down()
    {      
        StartCoroutine("ScaleDown");
    }

    private IEnumerator IncreaseScale()
    {
        //Scale the object up
        while (transform.localScale.x < MaxScale)
        {
            transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * ScaleFactor;
            yield return null;
        }

        //after object reached maxscale, scale it down.
        while (transform.localScale.x > NormalScale)
        {
            transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * ScaleFactor;
            yield return null;
        }
    }

    private IEnumerator ScaleDown()
    {        
        while (transform.localScale.x > Minscale)
        {          
            transform.localScale -= new Vector3(1,1,1)*Time.deltaTime*ScaleFactor;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
