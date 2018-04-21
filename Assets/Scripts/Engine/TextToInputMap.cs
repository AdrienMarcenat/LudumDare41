using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class TextToInputMap : Singleton<TextToInputMap>
{
	private Dictionary<string, UnityEvent> m_TextToInputMap;
	[SerializeField] private string m_FileName;

	void Start ()
	{
		base.Awake ();
		m_TextToInputMap = new Dictionary<string, UnityEvent> ();
		FillCommands ();
	}

	private void FillCommands ()
	{
		char[] separators = { ':' };
		#if UNITY_EDITOR
		m_FileName = "Assets/" + m_FileName;
		#endif

		string[] lines = File.ReadAllLines (m_FileName);

		for (int i = 0; i < lines.Length; i++)
		{
			string[] datas = lines [i].Split (separators);

			// If there is an error in print a debug message
			if (datas.Length != 2)
			{
				Debug.Log ("Invalid number of data line " + i + " excpeting 2, got " + datas.Length);
				return;
			}
			string command = datas [0].ToLower ();
			string eventName = datas [1].ToLower ();
			UnityEvent e = EventManager.RegisterEvent (eventName);
			AddEntry (command, e);
		}
	}

	private void AddEntry (string command, UnityEvent e)
	{
		m_TextToInputMap.Add (command, e);
	}

	public void FireCommand (string command)
	{
		UnityEvent e = null;
		if (m_TextToInputMap.TryGetValue ('"' + command + '"', out e))
		{
			e.Invoke ();
		}
	}
}

