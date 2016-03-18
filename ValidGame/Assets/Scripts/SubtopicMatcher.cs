using UnityEngine;
/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Attached to subtopic card holders to compare the placed cards with their matchcode,
///             Also tracks whether the slot is currently occupied or not.
/// </summary>
public class SubtopicMatcher : MonoBehaviour
{
    private string _MatchCode;
    private bool _Occupied = false;
    // Use this for initialization

    void Awake()
    {
        string parentName = transform.parent.name;
        string matchCode = parentName;
        _MatchCode = matchCode;
    }

    public bool Occupied
    {
        get { return _Occupied; }
        set { _Occupied = value; }
    }

    public string MatchCode
    {
        get { return _MatchCode; }
    }
}