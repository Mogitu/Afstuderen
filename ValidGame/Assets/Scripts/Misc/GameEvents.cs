/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Container for all game events.
/// TODO    :   New/unused events need to be added/removed.
/// </summary>
public class EVENT_TYPE
{
    public const short ENABLE = 1;
    public const short DISABLE = 2;
    public const short SENDCHAT = 3;
    public const short RECEIVECHATNETWORK = 4;
    public const short RECEIVESCORE = 5;
    public const short RECEIVESCORENETWORK = 6;
    public const short SENDSCORE = 7;
    public const short SENDSCORENETWORK = 8;
    public const short PLAYERJOINED = 9;
    public const short PLAYERLEFT = 10;
}
