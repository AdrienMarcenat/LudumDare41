using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class GameFlowDialogueState : FSMState
{
	private DialogManager m_DialogManager;

	protected override void Awake ()
	{
		ID = (int)GameFlowStates.ID.Dialogue;
		base.Awake ();
	}

	public override void Enter ()
	{
		GameManager.instance.currentState = ID;
		m_DialogManager = GameObject.Find ("DialogueManager").GetComponent<DialogManager> ();
	}

	public override bool StateUpdate ()
	{
		if (Input.GetButtonDown ("Submit"))
			NextSentence ();

		return true;
	}

	public override void Exit ()
	{
		GameManager.instance.currentState = 0;
		GameObject.Find ("LevelManager").GetComponent<LevelGenerator> ().isWaiting = false;
	}

	void NextSentence ()
	{
		// If the dialogue has ended we can resume the level generation
		if (m_DialogManager.DisplayNextSentence ()) {
			requestStackPop ();
		}
	}
}

