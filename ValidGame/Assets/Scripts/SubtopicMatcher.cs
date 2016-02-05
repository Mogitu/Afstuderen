using UnityEngine;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Attached to subtopic card holders to compare the placed cards with their matchcode,
///             Also tracks whether the slot is currently occupied or not.
/// </summary>
public class SubtopicMatcher : MonoBehaviour
{
    // Use this for initialization
    public string matchCode;
    public bool occupied = false;
}


