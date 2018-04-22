using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class DialogueManager : Singleton<DialogueManager>
{
	[SerializeField] private Text m_DialogeText;
	[SerializeField] private Text m_NameText;
	[SerializeField] private Animator m_Animator;

	private Queue<Dialogue.Sentence> m_Sentences;
	private static string dialogueFileName = "Datas/Dialogues.txt";

	void Start ()
	{
		m_Sentences = new Queue<Dialogue.Sentence> ();
	}

	public void StartDialogue (Dialogue dialogue)
	{
        return;
		m_Animator.SetBool ("IsOpen", true);

		m_Sentences.Clear ();

		foreach (Dialogue.Sentence sentence in dialogue.sentences)
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
		Dialogue.Sentence sentence = m_Sentences.Dequeue ();
		m_NameText.text = sentence.name;
		StartCoroutine (TypeSentence (sentence.sentence));
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

	public void TriggerDialogue (string tag)
	{
		Dialogue dialogue = new Dialogue ();

		char[] separators = { ':' };
		string filename = dialogueFileName;
		#if UNITY_EDITOR
		filename = "Assets/" + dialogueFileName;
		#endif

		string[] lines = File.ReadAllLines (filename);

		int dialogueBeginning = 0;
		int dialogueEnd = 0;
		for (int i = 0; i < lines.Length; i++)
		{
			string[] datas = lines [i].Split (separators);

			// If ther is a single word it is a dialog tag
			if (datas.Length == 1 && datas [0] == tag)
			{
				dialogueBeginning = i + 2;
			}
			// We then seek for the a ] that signals the end of the dialogue
			if (dialogueBeginning > 0 && datas.Length == 1 && datas [0] == "]")
			{
				dialogueEnd = i;
			}
		}
		if (dialogueBeginning == 0)
		{
			Debug.Log ("Could not find dialogue with tag " + tag);
		}

		for (int i = dialogueBeginning; i < dialogueEnd; i++)
		{
			string[] datas = lines [i].Split (separators);
			if (datas.Length != 2)
			{
				Debug.Log ("Invalid number of data line " + i + " expecting 2, got " + datas.Length);
				return;
			}
			dialogue.sentences.Add (new Dialogue.Sentence (datas [0], datas [1]));
		}

		StartDialogue (dialogue);
	}
}

