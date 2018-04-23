using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class DialogManager : MonoBehaviour
{
	[SerializeField] private Text m_DialogText;
	[SerializeField] private Text m_NameText;
	[SerializeField] private DialogGUI m_DialogGUI;

	private Queue<Dialog.Sentence> m_Sentences;
	private static string dialogueFileName = "Datas/Dialogues.txt";
	private Coroutine m_CloseDialogueGuiAfterSecondsRoutine;

	void Start ()
	{
		m_Sentences = new Queue<Dialog.Sentence> ();
	}

	public void StartDialogue (Dialog dialogue)
	{
		m_DialogGUI.Open (2);

		m_Sentences.Clear ();

		foreach (Dialog.Sentence sentence in dialogue.sentences) {
			m_Sentences.Enqueue (sentence);
		}

		DisplayNextSentence ();
	}

	public bool DisplayNextSentence ()
	{
		if (m_Sentences.Count == 0) {
			EndDialogue ();
			return true;
		}
		StopAllCoroutines ();
		Dialog.Sentence sentence = m_Sentences.Dequeue ();
		m_NameText.text = sentence.name;
		StartCoroutine (TypeSentence (sentence.sentence));

		return false;
	}

	IEnumerator TypeSentence (string sentence)
	{
		m_DialogText.text = "";
		foreach (char letter in sentence.ToCharArray()) {
			m_DialogText.text += letter;
			yield return null;
		}
	}

	public void EndDialogue ()
	{
		if (m_DialogGUI.isActiveAndEnabled)
			m_DialogGUI.Close ();
	}

	public void TriggerDialogue (string tag)
	{
		Dialog dialogue = new Dialog ();

		char[] separators = { ':' };
		string filename = dialogueFileName;
		#if UNITY_EDITOR
		filename = "Assets/" + dialogueFileName;
		#endif

		string[] lines = File.ReadAllLines (filename);

		int dialogueBeginning = 0;
		int dialogueEnd = 0;
		for (int i = 0; i < lines.Length; i++) {
			string[] datas = lines [i].Split (separators);

			// If ther is a single word it is a dialog tag
			if (datas.Length == 1 && datas [0] == tag) {
				dialogueBeginning = i + 2;
			}
			// We then seek for the a ] that signals the end of the dialogue
			if (dialogueBeginning > 0 && datas.Length == 1 && datas [0] == "]") {
				dialogueEnd = i;
				break;
			}
		}
		if (dialogueBeginning == 0) {
			Debug.Log ("Could not find dialogue with tag " + tag);
		}

		for (int i = dialogueBeginning; i < dialogueEnd; i++) {
			string[] datas = lines [i].Split (separators);
			if (datas.Length != 2) {
				Debug.Log ("Invalid number of data line " + i + " expecting 2, got " + datas.Length);
				return;
			}
			dialogue.sentences.Add (new Dialog.Sentence (datas [0], datas [1]));
		}

		StartDialogue (dialogue);
	}

	public void CloseDialogueGuiAfterSeconds (float seconds)
	{
		if (m_CloseDialogueGuiAfterSecondsRoutine != null)
			StopCoroutine (m_CloseDialogueGuiAfterSecondsRoutine);
		m_CloseDialogueGuiAfterSecondsRoutine = StartCoroutine (CloseDialogueGuiAfterSecondsRoutine (seconds));
	}

	IEnumerator CloseDialogueGuiAfterSecondsRoutine (float seconds)
	{
		yield return new WaitForSecondsRealtime (seconds);
		if (m_DialogGUI != null)
			m_DialogGUI.Close ();
	}
}

