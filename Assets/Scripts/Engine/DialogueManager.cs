using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : Singleton<DialogueManager>
{
	public Text m_DialogeText;
	public Text m_NameText;
	public Animator m_Animator;

	private Queue<string> m_Sentences;

	void Start ()
	{
		m_Sentences = new Queue<string> ();
	}

	public void StartDialogue (Dialogue dialogue)
	{
		m_Animator.SetBool ("IsOpen", true);
		m_NameText.text = dialogue.m_Name;

		m_Sentences.Clear ();

		foreach (string sentence in dialogue.m_Sentences)
		{
			m_Sentences.Enqueue (sentence);
		}

		DisplayNextSentence ();
	}

	public void DisplayNextSentence ()
	{
		if (m_Sentences.Count == 0)
		{
			EndDialogue ();
			return;
		}
		StopAllCoroutines ();
		StartCoroutine (TypeSentence (m_Sentences.Dequeue ()));
	}

	IEnumerator TypeSentence (string sentence)
	{
		m_DialogeText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			m_DialogeText.text += letter;
			yield return null;
		}
	}

	public void EndDialogue ()
	{
		m_Animator.SetBool ("IsOpen", false);
	}
}

