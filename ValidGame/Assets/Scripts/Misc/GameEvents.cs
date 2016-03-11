/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Container for all game events.
/// TODO    :   New/unused events need to be added/removed.
/// </summary>
public class GameEvents
{
    public const short Enable = 1;
    public const short Disable = 2;
    public const short SendSchat = 3;
    public const short ReceiveChatNetwork = 4;
    public const short ReceiveScore = 5;
    public const short ReceiveScoreNetwork = 6;
    public const short SendScore = 7;
    public const short SendScoreNetwork = 8;
    public const short PlayerJoined = 9;
    public const short PlayerLeft = 10;

    public const short SendCardToOpponent = 11;
    public const short CardReceivedFromOpponent = 12;
    public const short ReceivedTeamType = 13;
    public const short SuccesfullConnection = 14;
}
