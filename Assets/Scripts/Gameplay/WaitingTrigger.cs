using UnityEngine;
using System.Collections;

public class WaitingTrigger : MonoBehaviour
{
	void OnDisable ()
	{
		Debug.Log ("end waiting");
		GameObject.Find ("LevelManager").GetComponent<LevelGenerator> ().isWaiting = false;
	}
}

