using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   .....
/// </summary>
public class EventManager :MonoBehaviour
{   
    public delegate void OnEvent(EVENT_TYPE Event_Type, Component Sender, object Param = null);

    private Dictionary<EVENT_TYPE, List<OnEvent>> Listeners = new Dictionary<EVENT_TYPE, List<OnEvent>>();  

    public void AddListener(EVENT_TYPE Event_Type, OnEvent Listener)
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

    public void PostNotification(EVENT_TYPE Event_Type, Component Sender, object Param = null)
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

    public void RemoveEvent(EVENT_TYPE Event_Type)
    {
        Listeners.Remove(Event_Type);
    }

    public void RemoveRedundancies()
    {        
        Dictionary<EVENT_TYPE, List<OnEvent>> TmpListeners = new Dictionary<EVENT_TYPE, List<OnEvent>>();        
        foreach (KeyValuePair<EVENT_TYPE, List<OnEvent>> Item in Listeners)
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

public enum EVENT_TYPE
{
    ENABLE,
    DISABLE,
    SENDCHAT,
    RECEIVECHAT,
    RECEIVESCORE, 
    SENDSCORE
}
