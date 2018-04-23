using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class WrongCommandHandle : MonoBehaviour
{
	private UnityAction<CommandModifier> m_ActionWrongCommand;
	private DialogManager m_DialogManager;
	private int m_WrongCommandNumber;
	private static string[] names = { "Bob", "Linda", "Tom", "Carl", "Cindy" };

	[SerializeField] private int m_WrongCommandNumberBeforeDialogue;

	void Start ()
	{
		m_DialogManager = GameObject.Find ("DialogueManager").GetComponent<DialogManager> ();
		m_ActionWrongCommand = new UnityAction<CommandModifier> (WrongCommand);
		m_WrongCommandNumber = 0;
		EventManager.Register ("WrongCommand", m_ActionWrongCommand);
	}

	void OnDisable ()
	{
		EventManager.Unregister ("WrongCommand", m_ActionWrongCommand);
	}

	void WrongCommand (CommandModifier cm)
	{
		m_WrongCommandNumber++;
		if (m_WrongCommandNumber == m_WrongCommandNumberBeforeDialogue) {
			m_WrongCommandNumber = 0;
			int name = Random.Range (0, 5);
			int conv = Random.Range (1, 6);
			m_DialogManager.TriggerDialogue ("WrongCommand" + names [name] + conv);
		}
	}
}

