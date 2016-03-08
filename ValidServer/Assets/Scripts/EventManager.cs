using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Eventmanager, place in scene and drag it in a referenceslot on a gameobject that needs the evenmanager.
/// </summary>
public class EventManager : MonoBehaviour
{
    public delegate void OnEvent(short event_Type, Component sender, object param = null);

    private Dictionary<short, List<OnEvent>> Listeners = new Dictionary<short, List<OnEvent>>();

    public void AddListener(short event_Type, OnEvent listener)
    {
        List<OnEvent> listenList = null;
        if (Listeners.TryGetValue(event_Type, out listenList))
        {
            listenList.Add(listener);
            return;
        }
        listenList = new List<OnEvent>();
        listenList.Add(listener);
        Listeners.Add(event_Type, listenList);
    }

    public void PostNotification(short event_Type, Component sender, object param = null)
    {
        List<OnEvent> listenList = null;
        if (!Listeners.TryGetValue(event_Type, out listenList))
        {
            return;
        }

        for (int i = 0; i < listenList.Count; i++)
        {
            if (!listenList[i].Equals(null))
            {
                listenList[i](event_Type, sender, param);
            }
        }
    }

    public void RemoveEvent(short event_Type)
    {
        Listeners.Remove(event_Type);
    }

    //Removes all redundantly added listeners.
    public void RemoveRedundancies()
    {
        Dictionary<short, List<OnEvent>> tmpListeners = new Dictionary<short, List<OnEvent>>();
        foreach (KeyValuePair<short, List<OnEvent>> Item in Listeners)
        {
            for (int i = Item.Value.Count - 1; i >= 0; i--)
            {
                if (Item.Value[i].Equals(null))
                    Item.Value.RemoveAt(i);
            }
            if (Item.Value.Count > 0)
                tmpListeners.Add(Item.Key, Item.Value);
        }
        Listeners = tmpListeners;
    }

    public void OnLevelWasLoaded()
    {
        RemoveRedundancies();
    }
}

