using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class PlayerEventManager : MonoBehaviour
{
	public static string MoveLeft = "MoveLeft";
	public static string MoveRight = "MoveRight";
	public static string MoveStop = "MoveStop";
	public static string Fire = "Fire";
	public static string Laser = "Laser";
	public static string Pause = "pause";
	public static string Resume = "resume";
	public static string Menu = "menu";
	public static string NextLevel = "NextLevel";
	public static string Retry = "retry";
	public static string Autodestruction = "autodestruction";
	public static string Shield = "shield";
	public static string StopMusic = "stop music";
	public static string Time = "Time";

	private LetterInventory m_LetterInventory;

	void Start ()
	{
		m_LetterInventory = GetComponent<LetterInventory> ();
	}

	protected void Update ()
	{
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Letter") {
			m_LetterInventory.AddLetter (other.gameObject.GetComponent<LetterItem> ().letter);
			Destroy (other.gameObject);
		}
	}
}

