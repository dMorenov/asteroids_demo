using System;
using System.Collections.Generic;
using System.Linq;

public struct MessagesCOmparer : IEqualityComparer<Messages>
{
    public bool Equals(Messages x, Messages y)
    {
        return x == y;
    }

    public int GetHashCode(Messages obj)
    {
        // you need to do some thinking here,
        return (int)obj;
    }
}

static internal class MessengerInternal
{
    readonly public static Dictionary<Messages, Delegate> eventTable =
        new Dictionary<Messages, Delegate>(new MessagesCOmparer());

    static public void AddListener(Messages eventType, Delegate callback)
    {
        MessengerInternal.OnListenerAdding(eventType, callback);
        eventTable[eventType] = Delegate.Combine(eventTable[eventType], callback);
    }

    static public void RemoveListener(Messages eventType, Delegate handler)
    {
        if (!eventTable.ContainsKey(eventType))
        {
            return;
        }
        MessengerInternal.OnListenerRemoving(eventType, handler);
        eventTable[eventType] = Delegate.Remove(eventTable[eventType], handler);
        MessengerInternal.OnListenerRemoved(eventType);
    }

    static public T[] GetInvocationList<T>(Messages eventType)
    {
        Delegate d;

        if (eventTable.TryGetValue(eventType, out d))
        {
            if (d != null)
            {
                return d.GetInvocationList().Cast<T>().ToArray();
            }
            else
            {
                throw MessengerInternal.CreateBroadcastSignatureException(eventType);
            }
        }
        return null;
    }

    static public void OnListenerAdding(Messages eventType, Delegate listenerBeingAdded)
    {
        if (!eventTable.ContainsKey(eventType))
        {
            eventTable.Add(eventType, null);
        }

        var d = eventTable[eventType];
        if (d != null && d.GetType() != listenerBeingAdded.GetType())
        {
            throw new ListenerException(string.Format(
                "Attempting to add listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being added has type {2}",
                eventType, d.GetType().Name, listenerBeingAdded.GetType().Name));
        }
    }

    static public void OnListenerRemoving(Messages eventType, Delegate listenerBeingRemoved)
    {
        if (eventTable.ContainsKey(eventType))
        {
            var d = eventTable[eventType];

            if (d == null)
            {
                throw new ListenerException(string.Format("Attempting to remove listener with for event type {0} but current listener is null.", eventType));
            }
            else if (d.GetType() != listenerBeingRemoved.GetType())
            {
                throw new ListenerException(string.Format("Attempting to remove listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being removed has type {2}", eventType, d.GetType().Name, listenerBeingRemoved.GetType().Name));
            }
        }
        else
        {
            throw new ListenerException(string.Format("Attempting to remove listener for type {0} but Messenger doesn't know about this event type.", eventType));
        }
    }

    static public void OnListenerRemoved(Messages eventType)
    {
        if (eventTable[eventType] == null)
        {
            eventTable.Remove(eventType);
        }
    }

    static public void OnBroadcasting(Messages eventType)
    {
    }

    static public BroadcastException CreateBroadcastSignatureException(Messages eventType)
    {
        return new BroadcastException(string.Format(
            "Broadcasting message {0} but listeners have a different signature than the broadcaster.", eventType));
    }

    public class BroadcastException : Exception
    {
        public BroadcastException(string msg)
            : base(msg)
        {
        }
    }

    public class ListenerException : Exception
    {
        public ListenerException(string msg)
            : base(msg)
        {
        }
    }
}

// No parameters
static public class Messenger
{
    static public void AddListener(Messages eventType, Action handler)
    {
        MessengerInternal.AddListener(eventType, handler);
    }

    static public void RemoveListener(Messages eventType, Action handler)
    {
        MessengerInternal.RemoveListener(eventType, handler);
    }

    static public void Broadcast(Messages eventType)
    {
        MessengerInternal.OnBroadcasting(eventType);
        var invocationList = MessengerInternal.GetInvocationList<Action>(eventType);
        if (invocationList == null)
        {
            return;
        }
        foreach (var callback in invocationList)
        {
            callback.Invoke();
        }
    }
}

// One parameter
static public class Messenger<T>
{
    static public void AddListener(Messages eventType, Action<T> handler)
    {
        MessengerInternal.AddListener(eventType, handler);
    }

    static public void RemoveListener(Messages eventType, Action<T> handler)
    {
        MessengerInternal.RemoveListener(eventType, handler);
    }

    static public void RemoveListener<TReturn>(Messages eventType, Func<T, TReturn> handler)
    {
        MessengerInternal.RemoveListener(eventType, handler);
    }

    static public void Broadcast(Messages eventType, T arg1)
    {
        MessengerInternal.OnBroadcasting(eventType);
        var invocationList = MessengerInternal.GetInvocationList<Action<T>>(eventType);
        if (invocationList == null)
        {
            return;
        }

        foreach (var callback in invocationList)
        {
            callback.Invoke(arg1);
        }
    }
}

