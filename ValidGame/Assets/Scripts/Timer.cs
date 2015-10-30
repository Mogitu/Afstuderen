//----------------------------------------------------------------------------------
// Class : Timer
// Desc     : Simple timer class that will always count back down to zero if set
//           to a non zero amount.
// ----------------------------------------------------------------------------------
public class Timer
{
    // Internal timer
    private float timer = 0.0f;

    // ------------------------------------------------------------------------------
    // Contructor
    // Desc    :    Initialize timer to zero
    // ------------------------------------------------------------------------------
    public Timer()
    {
        timer=0.0f;
    }
   
    // ------------------------------------------------------------------------------
    // Name    :    Tick
    // Desc    :    Perform an update of the timer by passing the number of seconds
    //            to update.
    // -----------------------------------------------------------------------------
    public void Tick( float seconds )
    {
        // Decrement timer by seconds passed
        timer -= seconds;
        
        // Clamp to zero
        if (timer<0.0f) timer = 0.0f;
    }

    // ------------------------------------------------------------------------------
    // Name    :    AddTimer
    // Desc    :    Add seconds to the timer
    // ------------------------------------------------------------------------------
    public void AddTime ( float seconds )
    {
        timer+=seconds;
    }

    // ------------------------------------------------------------------------------
    // Name    :    GetTime
    // Desc    :    Get value of timer
    // ------------------------------------------------------------------------------
    public float GetTime()
    {
        return timer;
    }
}