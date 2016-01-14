using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace VALIDGame
{
    public class PopupHandler : MonoBehaviour
    {
        private Text texty;
        public float duration;
        private bool running;
        private float currentTime = 0.0f;

        // Use this for initialization
        void Start()
        {
            texty = GetComponent<Text>();
            texty.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (running)
            {
                currentTime += Time.deltaTime;
                if (currentTime >= duration)
                {
                    texty.gameObject.SetActive(false);
                    currentTime = 0.0f;
                }
            }
        }

        public void Show(string message)
        {
            running = true;
            texty.gameObject.SetActive(true);
            texty.text = message;
        }
    }
}

