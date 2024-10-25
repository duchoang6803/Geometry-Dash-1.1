using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : Singleton<Observer>
{
    private Dictionary<EventID, Action<object>> Listeners = new Dictionary<EventID, Action<object>>();

    public void AddObserver(EventID gameMode,Action<object> action)
    {
        if (!Listeners.ContainsKey(gameMode))
        {
            Listeners[gameMode] = action;
        }
        else
        {
            Listeners[gameMode] += action;
        }
    }

    public void RemoveObserver(EventID gameMode,Action<object> action)
    {
        if (!Listeners.ContainsKey(gameMode)) return;

        Listeners[gameMode] -= action;
    }

    public void Notify(EventID gameMode,object data)
    {
        if (Listeners.ContainsKey(gameMode))
        {
            Listeners[gameMode]?.Invoke(data);
        }
    }
}
