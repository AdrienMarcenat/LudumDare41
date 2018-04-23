using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class SituatonalDialogueHandler : MonoBehaviour
{
	private UnityAction<CommandModifier> m_ActionWrongCommand;
	private UnityAction<CommandModifier> m_ActionHit;
	private UnityAction<CommandModifier> m_ActionLowHealth;
	private UnityAction<CommandModifier> m_ActionNextSentence;

	private DialogManager m_DialogManager;
	private int m_WrongCommandNumber;
	private static string[] names = { "Bob", "Linda", "Tom", "Carl", "Cindy" };

	[SerializeField] private int m_WrongCommandNumberBeforeDialogue;

	void Start ()
	{
		m_DialogManager = GameObject.Find ("DialogueManager").GetComponent<DialogManager> ();
		m_WrongCommandNumber = 0;

		m_ActionWrongCommand = new UnityAction<CommandModifier> (WrongCommand);
		m_ActionHit = new UnityAction<CommandModifier> (Hit);
		m_ActionLowHealth = new UnityAction<CommandModifier> (LowHealth);
		m_ActionNextSentence = new UnityAction<CommandModifier> (NextSentence);

		EventManager.Register ("WrongCommand", m_ActionWrongCommand);
		EventManager.Register ("Hit", m_ActionHit);
		EventManager.Register ("LowHealth", m_ActionLowHealth);
		EventManager.Register ("EnterPressed", m_ActionNextSentence);
	}

	void OnDisable ()
	{
		EventManager.Unregister ("WrongCommand", m_ActionWrongCommand);
		EventManager.Unregister ("Hit", m_ActionHit);
		EventManager.Unregister ("LowHealth", m_ActionLowHealth);
		EventManager.Unregister ("EnterPressed", m_ActionNextSentence);
	}

	void WrongCommand (CommandModifier cm)
	{
		m_WrongCommandNumber++;
		if (m_WrongCommandNumber == m_WrongCommandNumberBeforeDialogue) {
			m_WrongCommandNumber = 0;
			int name = Random.Range (0, 5);
			int index = Random.Range (1, 6);
			m_DialogManager.TriggerDialogue ("WrongCommand" + names [name] + index);
		}
	}

	void Hit (CommandModifier cm)
	{
		int index = Random.Range (1, 10);
		m_DialogManager.TriggerDialogue ("Hit" + index);
	}

	void LowHealth (CommandModifier cm)
	{
		int index = Random.Range (1, 6);
		m_DialogManager.TriggerDialogue ("LowHealth" + index);
	}

	void NextSentence (CommandModifier cm)
	{
		m_DialogManager.DisplayNextSentence ();
	}
}

