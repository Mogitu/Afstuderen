using UnityEngine;
using System.Collections;

namespace AMCTools
{
    public class ObjectOutline : MonoBehaviour
    {
        // Use this for initialization  
        public Color hoverColor;
        private Color normalColor;
        private Renderer objectRenderer;

        void Start()
        {
            objectRenderer = GetComponent<Renderer>();
            normalColor = objectRenderer.material.color;
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnMouseEnter()
        {
            if (enabled)
                objectRenderer.material.color = hoverColor;
        }

        void OnMouseExit()
        {
            if (enabled)
                objectRenderer.material.color = normalColor;
        }
    }

}
