using UnityEngine;
using System.Collections;

namespace GameFlowStates
{
	public enum ID
	{
		Menu = 0,
		Level = 1,
		Boss = 2,
		Dialogue = 3,
		Pause = 4,
		EndLevel = 5,
		GameOver = 6,
		Loading = 7
	}
}

public class GameFlowFSM : FSM
{
	protected override void Awake ()
	{
		base.Awake ();
		PushState ((int)GameFlowStates.ID.Menu);
	}
}
