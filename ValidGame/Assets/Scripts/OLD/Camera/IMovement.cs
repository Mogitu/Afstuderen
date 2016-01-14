using UnityEngine;

namespace VALIDGame
{
    //----------------------------------------------------------------------------------
    // Interface    : IMovement
    // Desc         : Implement this interface to set different control behaviours on the camera controller. 
    // -----------------
    public interface IMovement
    {
        void Move(CameraController cont);
    }
}
