using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/// <summary>
/// Author       :   Maikel van Munsteren
/// Desc         :   Eventmanager, place in scene and drag it in a referenceslot on a gameobject that needs the evenmanager.
/// Notes/TODO   :   Because it extends monobehaviour the manager is in fact an "gameobject" and can be referenced as such; drag it into public slots
///                  or use "Find or GetComponent, etc". Alternative is making the class static/singleton so it can be more easily accessed and setup, at the cost of abusing it.
/// </summary>
public class EventManager : MonoBehaviour
{
    public delegate void OnEvent(short eventType, Component sender, object param = null);

    private Dictionary<short, List<OnEvent>> Listeners = new Dictionary<short, List<OnEvent>>();
    private delegate void Jan();

    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }   

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

    void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {        
        RemoveRedundancies();
    }
}

