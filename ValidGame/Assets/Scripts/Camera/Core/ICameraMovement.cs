/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Implement this interface to set different control behaviours on the camera controller. 
/// </summary>
namespace AMC.Camera
{
    public interface ICameraMovement
    {
        void Move(ICameraController cont);
    }
}
