using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class PlayerGodState : FSMState
{
	private LetterInventory m_LetterInventory;
	private UnityAction<CommandModifier> m_ActionGodState;

	protected override void Awake ()
	{
		ID = (int)PlayerStates.ID.God;
		base.Awake ();

		m_LetterInventory = GetComponent<LetterInventory> ();
		m_ActionGodState = new UnityAction<CommandModifier> (ExitGodState);
	}

	public override void Enter ()
	{
		EventManager.Register (PlayerEventManager.Resume, m_ActionGodState);
		m_LetterInventory.GodMode ();
	}

	public override void Exit ()
	{
		m_LetterInventory.NormalMode ();
		EventManager.Unregister (PlayerEventManager.Resume, m_ActionGodState);
	}

	private void ExitGodState (CommandModifier cm)
	{
		requestStackPop ();
		requestStackPush ((int)PlayerStates.ID.Normal);
	}
}

