using UnityEngine;
using System.Collections;

public class DialogueTrigger : MonoBehaviour
{
	public Dialogue m_Dialogue;

	public void TriggerDialogue ()
	{
		DialogueManager.instance.StartDialogue (m_Dialogue);
	}
}

