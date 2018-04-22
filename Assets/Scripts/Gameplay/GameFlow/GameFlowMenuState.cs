using UnityEngine;
using System.Collections;

public class GameFlowMenuState : FSMState
{
	[SerializeField] GameObject blinkingText;
	[SerializeField] float blinkingRate;

	protected override void Awake ()
	{
		ID = (int)GameFlowStates.ID.Menu;
		base.Awake ();
	}

	public override void Enter ()
	{
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
			GameManager.instance.nextState = (int)GameFlowStates.ID.Level;
			requestStackPush ((int)GameFlowStates.ID.Loading);
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
		blinkingText.SetActive (!blinkingText.activeSelf);
	}
}



