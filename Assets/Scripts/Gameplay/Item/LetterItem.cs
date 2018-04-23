using UnityEngine;
using System.Collections;
using UnityEngine.Analytics;
using UnityEditor;

public class LetterItem : MonoBehaviour
{
	public char letter;

	[SerializeField] private SpriteRenderer m_LetterSprite;
	[SerializeField] private float m_Speed;
	[SerializeField] private float m_Acceleration;
	private Transform m_Target;

	void Start ()
	{
		int charIndex = LetterInventory.ms_AllLetters.IndexOf (letter);
		m_LetterSprite.sprite = RessourceManager.instance.LoadSprite ("Game/Item/chars_spaced", charIndex);
		m_Target = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	void Update ()
	{
		m_Speed += m_Acceleration;
		transform.position = Vector3.MoveTowards (transform.position, m_Target.position, m_Speed * Time.deltaTime);
	}
}

