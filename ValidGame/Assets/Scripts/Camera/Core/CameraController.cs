using UnityEngine;
/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   
/// </summary>
namespace AMC.Camera
{
    public abstract class CameraController : MonoBehaviour, ICameraController
    {
        private ICameraMovement activeMovement;

        void Update()
        {
            HandleInput();
        }
        public abstract void HandleInput();

        public void SetCameraMovement(ICameraMovement movement)
        {
            activeMovement = movement;
            activeMovement.Move(this);
        }
    }
}

