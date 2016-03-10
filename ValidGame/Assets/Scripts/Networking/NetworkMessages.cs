using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Contains all network event type. These map directly to custom defined messages below the container MsgTypes class.
/// </summary>
public class NetworkMessages
{
    public const short MsgChat = MsgType.Highest + 1;
    public const short MsgScore = MsgType.Highest + 2;
    public const short MsgPlayerJoined = MsgType.Highest + 3;
    public const short MsgPlayerLeft = MsgType.Highest + 4;
    public const short OpponentCard = MsgType.Highest + 5;
    public const short MsgTeamType = MsgType.Highest + 6;
}

//All custom messages below.
public class ChatMessage : MessageBase
{
    public string Text;
}

public class TeamTypeMessage : MessageBase
{
    public int TeamType;
}

public class ScoreMessage : MessageBase
{
    public int Score;
}

public class PlayerLeftMessage : MessageBase
{
    public string Text;    
}

public class PlayerJoinedMessage : MessageBase
{
    public string Text;
}

public class CardToOpponentMessage : MessageBase
{
    public string CardName;
    public Vector3 Position;
}