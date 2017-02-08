using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Base class for extending camera controls
/// </summary>
namespace AMC.Camera
{
    public abstract class CameraController : MonoBehaviour, ICameraController
    {
        private Dictionary<string, ICameraMovement> MovementSet;
        private ICameraMovement ActiveMovement;


        /// <summary>
        /// This function should be implemented by the extending class such that it makes sense for the deployment target to use
        /// </summary>
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

        /// <summary>
        /// Add a new movement to the list of patterns 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="movement"></param>
        public void AddMovementPattern(string key, ICameraMovement movement)
        {
            //no duplicates
            if (!MovementSet.ContainsKey(key))
            {
                MovementSet.Add(key, movement);
            }            
        }

        public ICameraMovement GetMovement(string key)
        {
            if (MovementSet.ContainsKey(key))
            {
                return MovementSet[key];
            }
            else
            {                
                return null;
            }           
        }

        public void SetCameraMovement(string key)
        {
            ActiveMovement = MovementSet[key];
            ActiveMovement.Move(this);
        }
    }
}