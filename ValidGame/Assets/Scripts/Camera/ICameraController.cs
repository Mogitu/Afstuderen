/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Interface to a controller that defines movement implementations etc.
/// TODO    :   Create functions etc.
/// </summary>
public interface ICameraController
{
    void SetCameraMovement(ICameraMovement movement);
    void HandleInput();
}

