using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   
/// </summary>
namespace AMC.Camera
{
    public abstract class CameraController : MonoBehaviour, ICameraController
    {
        private Dictionary<string, ICameraMovement> MovementSet;
        private ICameraMovement ActiveMovement;
        public abstract void HandleInput();

        void Awake()
        {
            MovementSet = new Dictionary<string, ICameraMovement>();
        }

        void Update()
        {           
            HandleInput();
        }

        public void SetCameraMovement(ICameraMovement movement)
        {
            ActiveMovement = movement;
            ActiveMovement.Move(this);
        }

        public void AddMovementPattern(string key, ICameraMovement movement)
        {
            MovementSet.Add(key, movement);
        }

        public ICameraMovement GetMovement(string key)
        {
            return MovementSet[key];
        }

        public void SetCameraMovement(string key)
        {
            ActiveMovement = MovementSet[key];
            ActiveMovement.Move(this);
        }
    }
}