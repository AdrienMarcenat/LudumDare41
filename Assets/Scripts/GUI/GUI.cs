using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI : MonoBehaviour
{
	public GUI instance
	{
		get;
		private set;
	}

	private HealthBar healthBar;
	private KeyPanel keyPanel;
	private CommandBar commandBar;

	void Awake ()
	{	
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Debug.LogError ("GUI already instanciated, the new one will be destroyed");
			Destroy (this);
		}
		healthBar = GetComponentInChildren<HealthBar> ();
		if (!healthBar)
		{
			Debug.LogWarning ("HealthBar not found in GUI");
		}
		keyPanel = GetComponentInChildren<KeyPanel> ();
		if (!keyPanel)
		{
			Debug.LogWarning ("KeyPanel not found in GUI");
		}
		commandBar = GetComponentInChildren<CommandBar> ();
		if (!keyPanel)
		{
			Debug.LogWarning ("CommandBar not found in GUI");
		}
	}

	void Start ()
	{
		SetCommand ("plz go right");
	}

	public void SetCommand (string command)
	{
		commandBar.SetText (command);
	}

	public void AddKey (char key)
	{
		keyPanel.Add (key);
	}

	public void RemoveKey (char key)
	{
		keyPanel.Remove (key);
	}
}
