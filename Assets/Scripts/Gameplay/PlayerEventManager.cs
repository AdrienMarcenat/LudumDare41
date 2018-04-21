using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class PlayerEventManager : MonoBehaviour
{
	public static string MoveLeft = "MoveLeft";
	public static string MoveRight = "MoveRight";

	void Start ()
	{
		
	}

	protected void Update ()
	{
		if (GameManager.pause)
			return;
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		
	}
}

