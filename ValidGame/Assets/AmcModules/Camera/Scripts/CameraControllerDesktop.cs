using UnityEngine;
/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Handles common camera functions.
/// TODO    :   Make this abstract.
/// </summary>
namespace AMC.Camera
{
    public class CameraControllerDesktop : CameraController
    {
        public float LookSpeed { get; private set; }
        public float MoveSpeed { get; private set; }
        public float ZoomSpeed { get; private set; }
        public float VerticalRangeUp = 15f;
        public float VerticalRangeDown = 40f;
        public float HorizontalRange = 90f;
        public float VerticalRotation = 0;
        public float HorizontalRotation = 0;
        public float MaxDistanceHorizontal = 0.3f;
        public float MaxVerticalDistance = 0.15f;

        // Use this for initialization
        void Start()
        {
            AddMovementPattern("Horizontal", new CameraDirectionalMovementDesktop());
            AddMovementPattern("Rotational", new CameraRotationalMovementDesktop());
            AddMovementPattern("Zoom", new CameraZoomMovementDesktop());
            SetPrefs();
        }

        public void SetPrefs()
        {
            ZoomSpeed = PlayerPrefs.GetFloat("ZoomSpeed", 5);
            LookSpeed = PlayerPrefs.GetFloat("LookSpeed", 5);
            MoveSpeed = PlayerPrefs.GetFloat("MoveSpeed", 5);
        }

        public override void HandleInput()
        {
            //move horizontally and vertically 
            if ((Input.GetMouseButton(0) && Input.GetMouseButton(1)) || Input.GetMouseButton(2))
            {
                SetCameraMovement("Horizontal");
                return;
            }

            //rotate the camera
            if (Input.GetMouseButton(1))
            {
                SetCameraMovement("Rotational");
                return;
            }

            //Zoom camera towards front
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                SetCameraMovement("Zoom");
            }
        }
    }
}
