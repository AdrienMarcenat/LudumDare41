using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class PlayerEventManager : MonoBehaviour
{
	public static string MoveLeft = "MoveLeft";
	public static string MoveRight = "MoveRight";
	public static string MoveStop = "MoveStop";
	public static string Fire = "Fire";

	private LetterInventory m_LetterInventory;

	void Start ()
	{
		m_LetterInventory = GetComponent<LetterInventory> ();
	}

	protected void Update ()
	{
		if (GameManager.pause)
			return;
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Letter")
		{
			m_LetterInventory.AddLetter (other.gameObject.GetComponent<LetterItem> ().letter);
			Destroy (other.gameObject);
		}
	}
}

