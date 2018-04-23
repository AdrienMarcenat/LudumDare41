using UnityEngine;
using System.Collections;

public class GameFlowMenuState : FSMState
{
	[SerializeField] GameObject blinkingText;
	[SerializeField] float blinkingRate;
	[SerializeField] private AudioClip m_MenuMusic;

	protected override void Awake ()
	{
		ID = (int)GameFlowStates.ID.Menu;
		base.Awake ();
	}

	public override void Enter ()
	{
		SoundManager.PlayMusic (m_MenuMusic);
		StartCoroutine (BlinkRoutine ());
	}

	public override bool StateUpdate ()
	{
		if (Input.GetButtonDown ("Escape")) {
			#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
			#else
			Application.Quit ();
			#endif
		}

		if (Input.GetButtonDown ("Space")) {
			StopAllCoroutines ();
			requestStateClear ();
			GameManager.instance.currentScene = 1;
			LevelGenerator.level = 1;
			GameManager.instance.nextState = (int)GameFlowStates.ID.Level;
			requestStackPush ((int)GameFlowStates.ID.Loading);
		}

		if (blinkingText == null) {
			blinkingText = GameObject.Find ("BlinkingText");
		}
		return true;
	}

	public override void Exit ()
	{
		
	}

	IEnumerator BlinkRoutine ()
	{
		while (true) {
			BlinkText ();
			yield return new WaitForSeconds (blinkingRate); 
		}
	}

	private void BlinkText ()
	{
		if (blinkingText != null) {
			blinkingText.SetActive (!blinkingText.activeSelf);
		}
	}
}



