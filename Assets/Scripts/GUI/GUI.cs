using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI : MonoBehaviour
{
	private HealthBar healthBar;

	public KeyPanel keyPanel { get; private set; }

	private CommandBar commandBar;
	public DialogGUI dialog;

	void Awake ()
	{	
		//base.Awake ();
		healthBar = GetComponentInChildren<HealthBar> ();
		if (!healthBar) {
			Debug.LogWarning ("HealthBar not found in GUI");
		}
		keyPanel = GetComponentInChildren<KeyPanel> ();
		if (!keyPanel) {
			Debug.LogWarning ("KeyPanel not found in GUI");
		}
		commandBar = GetComponentInChildren<CommandBar> ();
		if (!keyPanel) {
			Debug.LogWarning ("CommandBar not found in GUI");
		}
		GetComponent<Canvas> ().worldCamera = GameObject.FindWithTag ("MainCamera").GetComponent<Camera> ();
		healthBar.SetHealth (GameObject.FindGameObjectWithTag ("Player").GetComponent<Health> ());
	}

	public void SetCommand (string command)
	{
		commandBar.SetText (command);
	}
}
