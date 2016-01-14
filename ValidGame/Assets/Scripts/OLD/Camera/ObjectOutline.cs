using UnityEngine;
using System.Collections;

namespace AMCTools
{
    public class ObjectOutline : MonoBehaviour
    {
        // Use this for initialization  
        public Color hoverColor;
        private Renderer objectRenderer;
        private Material defaultMat;
        private Material highLightMat;

        void Start()
        {
            objectRenderer = GetComponent<Renderer>();
            highLightMat = (Material)Instantiate(objectRenderer.sharedMaterial);
            defaultMat = objectRenderer.sharedMaterial;
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnMouseEnter()
        {
            if (enabled)
            {
                objectRenderer.material = highLightMat;
                highLightMat.color = hoverColor;
            }
        }

        void OnMouseExit()        {
            if (enabled)              
                objectRenderer.material = defaultMat;
        }
    }
}
