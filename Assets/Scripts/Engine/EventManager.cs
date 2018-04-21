using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class EventManager : Singleton<EventManager>
{
	private Dictionary <string, UnityEvent> m_EventDictionary;

	void Init ()
	{
		if (m_EventDictionary == null)
		{
			m_EventDictionary = new Dictionary<string, UnityEvent> ();
		}
	}

	public static void Register (string eventName, UnityAction listener)
	{
		UnityEvent e = null;
		if (instance.m_EventDictionary.TryGetValue (eventName, out e))
		{
			e.AddListener (listener);
		}
		else
		{
			e = new UnityEvent ();
			e.AddListener (listener);
			instance.m_EventDictionary.Add (eventName, e);
		}
	}

	public static void Unregister (string eventName, UnityAction listener)
	{
		if (instance.m_EventDictionary == null)
		{
			return;
		}
			
		UnityEvent e = null;
		if (instance.m_EventDictionary.TryGetValue (eventName, out e))
		{
			e.RemoveListener (listener);
		}
	}

	public static void TriggerEvent (string eventName)
	{
		UnityEvent e = null;
		if (instance.m_EventDictionary.TryGetValue (eventName, out e))
		{
			e.Invoke ();
		}
	}

	public static UnityEvent FindEvent (string eventName)
	{
		UnityEvent e = null;
		instance.m_EventDictionary.TryGetValue (eventName, out e);
		return e;
	}
}