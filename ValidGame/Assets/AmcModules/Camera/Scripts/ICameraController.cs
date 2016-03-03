/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Interface to a controller that defines movement implementations etc.
/// TODO    :   Create functions etc.
/// </summary>
namespace AMC.Camera
{
    public interface ICameraController
    {
        void SetCameraMovement(ICameraMovement movement);
        void SetCameraMovement(string movement);
        void AddMovementPattern(string key, ICameraMovement movement);
        ICameraMovement GetMovement(string key);
        void HandleInput();
    }
}