using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Eventmanager, place in scene and drag it in a referenceslot on a gameobject that needs the evenmanager.
/// </summary>
public class EventManager : MonoBehaviour
{
    public delegate void OnEvent(short Event_Type, Component Sender, object Param = null);

    private Dictionary<short, List<OnEvent>> Listeners = new Dictionary<short, List<OnEvent>>();

    public void AddListener(short Event_Type, OnEvent Listener)
    {
        List<OnEvent> ListenList = null;
        if (Listeners.TryGetValue(Event_Type, out ListenList))
        {
            ListenList.Add(Listener);
            return;
        }
        ListenList = new List<OnEvent>();
        ListenList.Add(Listener);
        Listeners.Add(Event_Type, ListenList);
    }

    public void PostNotification(short Event_Type, Component Sender, object Param = null)
    {
        List<OnEvent> ListenList = null;
        if (!Listeners.TryGetValue(Event_Type, out ListenList))
        {
            return;
        }

        for (int i = 0; i < ListenList.Count; i++)
        {
            if (!ListenList[i].Equals(null))
            {
                ListenList[i](Event_Type, Sender, Param);
            }
        }
    }

    public void RemoveEvent(short Event_Type)
    {
        Listeners.Remove(Event_Type);
    }

    //Removes all redundantly added listeners.
    public void RemoveRedundancies()
    {
        Dictionary<short, List<OnEvent>> TmpListeners = new Dictionary<short, List<OnEvent>>();
        foreach (KeyValuePair<short, List<OnEvent>> Item in Listeners)
        {
            for (int i = Item.Value.Count - 1; i >= 0; i--)
            {
                if (Item.Value[i].Equals(null))
                    Item.Value.RemoveAt(i);
            }
            if (Item.Value.Count > 0)
                TmpListeners.Add(Item.Key, Item.Value);
        }
        Listeners = TmpListeners;
    }

    public void OnLevelWasLoaded()
    {
        RemoveRedundancies();
    }
}

