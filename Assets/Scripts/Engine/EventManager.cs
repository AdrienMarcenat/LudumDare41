using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

using Listener = UnityEngine.Events.UnityAction<CommandModifier>;

public class Command : UnityEvent<CommandModifier>
{
	
}

public class EventManager : Singleton<EventManager>
{
	private Dictionary <string, Command> m_EventDictionary;

    override protected void Awake ()
	{
		base.Awake ();
		if (m_EventDictionary == null)
		{
			m_EventDictionary = new Dictionary<string, Command> ();
		}
	}

	public static void Register (string eventName, Listener listener)
	{
		Command e = null;
		eventName = eventName.ToLower ();
		if (instance.m_EventDictionary.TryGetValue (eventName, out e))
		{
			e.AddListener (listener);
		}
		else
		{
			e = new Command ();
			e.AddListener (listener);
			instance.m_EventDictionary.Add (eventName, e);
		}
	}

	public static void Unregister (string eventName, Listener listener)
	{
		if (instance.m_EventDictionary == null)
		{
			return;
		}
			
		Command e = null;
		eventName = eventName.ToLower ();
		if (instance.m_EventDictionary.TryGetValue (eventName, out e))
		{
			e.RemoveListener (listener);
		}
	}

	public static void TriggerEvent (string eventName, CommandModifier modifier)
	{
		Command e = null;
		eventName = eventName.ToLower ();
		if (instance.m_EventDictionary.TryGetValue (eventName, out e))
		{
			e.Invoke (modifier);
		}
	}

	public static Command RegisterEvent (string eventName)
	{
		Command e = new Command ();
		instance.m_EventDictionary.Add (eventName, e);
		return e;
	}
}