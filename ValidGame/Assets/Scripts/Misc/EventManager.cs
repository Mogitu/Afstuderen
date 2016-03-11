using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Eventmanager, place in scene and drag it in a referenceslot on a gameobject that needs the evenmanager.
/// </summary>
public class EventManager : MonoBehaviour
{
    public delegate void OnEvent(short eventType, Component sender, object param = null);

    private Dictionary<short, List<OnEvent>> Listeners = new Dictionary<short, List<OnEvent>>();

    public void AddListener(short eventType, OnEvent listener)
    {
        List<OnEvent> listenList = null;
        if (Listeners.TryGetValue(eventType, out listenList))
        {
            listenList.Add(listener);
            return;
        }
        listenList = new List<OnEvent>();
        listenList.Add(listener);
        Listeners.Add(eventType, listenList);
    }

    public void PostNotification(short eventType, Component sender, object param = null)
    {
        List<OnEvent> listenList = null;
        if (!Listeners.TryGetValue(eventType, out listenList))
        {
            return;
        }

        for (int i = 0; i < listenList.Count; i++)
        {
            if (!listenList[i].Equals(null))
            {
                listenList[i](eventType, sender, param);
            }
        }
    }

    public void RemoveEvent(short eventType)
    {
        Listeners.Remove(eventType);
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

