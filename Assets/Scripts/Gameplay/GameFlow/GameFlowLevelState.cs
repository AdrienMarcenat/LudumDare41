﻿using UnityEngine;
using System.Collections;
using UnityEngine.Analytics;
using UnityEngine.Events;

public class GameFlowLevelState : FSMState
{
	[SerializeField] private AudioClip m_LevelMusic;

	private UnityAction<CommandModifier> m_ActionPause;
	private UnityAction<CommandModifier> m_ActionWaitDialogue;
	private UnityAction<CommandModifier> m_ActionEndLevel;

	protected override void Awake ()
	{
		ID = (int)GameFlowStates.ID.Level;
		base.Awake ();
		m_ActionPause = new UnityAction<CommandModifier> (Pause);
		m_ActionWaitDialogue = new UnityAction<CommandModifier> (WaitDialogue);
		m_ActionEndLevel = new UnityAction<CommandModifier> (EndLevel);
	}

	public override void Enter ()
	{
		EventManager.Register (PlayerEventManager.Pause, m_ActionPause);
		EventManager.Register ("WaitDialogue", m_ActionWaitDialogue);
		EventManager.Register ("EndLevel", m_ActionEndLevel);
		SoundManager.PlayMusic (m_LevelMusic);
		Debug.Log ("enter level " + LevelGenerator.level);
		GameObject.FindGameObjectWithTag ("Player").GetComponent<Health> ().GameOver += GameOver;
		LevelGenerator.Load ();
	}

	public override bool StateUpdate ()
	{
		if (Input.GetButtonDown ("Escape")) {
			EventManager.TriggerEvent (PlayerEventManager.Pause, new CommandModifier (1, 1, 1));
		}

		return true;
	}

	public override void Exit ()
	{
		GameObject.FindGameObjectWithTag ("Player").GetComponent<Health> ().GameOver -= GameOver;
		EventManager.Unregister (PlayerEventManager.Pause, m_ActionPause);
		EventManager.Unregister ("WaitDialogue", m_ActionWaitDialogue);
		EventManager.Unregister ("EndLevel", m_ActionEndLevel);
	}

	private void Pause (CommandModifier cm)
	{
		requestStackPush ((int)GameFlowStates.ID.Pause);
	}

	private void Boss ()
	{
		requestStackPop ();
		requestStackPush ((int)GameFlowStates.ID.Boss);
	}

	private void GameOver ()
	{
		requestStackPop ();
		requestStackPush ((int)GameFlowStates.ID.GameOver);
	}

	public void WaitDialogue (CommandModifier cm)
	{
		requestStackPush ((int)GameFlowStates.ID.Dialogue);
	}

	public void EndLevel (CommandModifier cm)
	{
		requestStackPop ();
		requestStackPush ((int)GameFlowStates.ID.EndLevel);
	}
}

