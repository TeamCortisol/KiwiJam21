using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    private Dictionary<GameEvent, UnityEvent<object>> eventDictionary;

    private static EventManager eventManager;

    private static EventManager Instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManager script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<GameEvent, UnityEvent<object>>();
        }
    }

    public static void Subscribe(GameEvent eventName, UnityAction<object> listener)
    {
        if (Instance.eventDictionary.TryGetValue(eventName, out UnityEvent<object> thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent<object>();
            thisEvent.AddListener(listener);
            Instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void Unsubscribe(GameEvent eventName, UnityAction<object> listener)
    {
        if (eventManager == null) return;
        if (Instance.eventDictionary.TryGetValue(eventName, out UnityEvent<object> thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void Emit(GameEvent eventName, object param = null)
    {
        if (Instance.eventDictionary.TryGetValue(eventName, out UnityEvent<object> thisEvent))
        {
            thisEvent.Invoke(param);
        }
    }
}
