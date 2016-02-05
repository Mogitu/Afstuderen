using UnityEngine.Networking;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Contains all network event type. These map directly to custom defined messages below the container MsgTypes class.
/// </summary>
public class MsgTypes
{
    public const short MSG_CHAT = MsgType.Highest + 1;
    public const short MSG_SCORE = MsgType.Highest + 2;
    public const short MSG_PLAYER_JOINED = MsgType.Highest + 3;
    public const short MSG_PLAYER_LEFT = MsgType.Highest + 4;
}

//All custom messages below.
public class ChatMessage : MessageBase
{
    public string text;
}

public class ScoreMessage : MessageBase
{
    public int score;
}

public class PlayerLeftMessage : MessageBase
{
    public string text;
}

public class PlayerJoinedMessage : MessageBase
{
    public string text;
}