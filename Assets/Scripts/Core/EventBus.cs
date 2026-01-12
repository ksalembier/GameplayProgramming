using System;
using System.Collections.Generic;

public static class EventBus
{
    private static Dictionary<string, Action<object>> events =
        new Dictionary<string, Action<object>>();

    public static void Subscribe(string eventName, Action<object> listener)
    {
        if (!events.ContainsKey(eventName))
            events[eventName] = null;

        events[eventName] += listener;
    }

    public static void Unsubscribe(string eventName, Action<object> listener)
    {
        if (events.ContainsKey(eventName))
            events[eventName] -= listener;
    }

    public static void Publish(string eventName, object payload = null)
    {
        if (events.ContainsKey(eventName))
            events[eventName]?.Invoke(payload);
    }
}
