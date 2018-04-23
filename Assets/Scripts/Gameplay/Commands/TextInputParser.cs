using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextInputParser : MonoBehaviour
{
	[SerializeField] private InputField m_InputField;
	[SerializeField] private LetterInventory m_LetterInventory;
	[SerializeField] private CommandModifierManager m_CommandModifierManager;
	private char[] m_Separators = { ' ' };

	void Awake ()
	{
		m_InputField.onEndEdit.AddListener (AcceptStringInput);
	}

	void Start ()
	{
		m_InputField.onValidateInput += delegate(string input, int charIndex, char addedChar) {
			return FilterChar (addedChar);
		};
		m_InputField.Select ();
		m_InputField.ActivateInputField ();
	}

	private char FilterChar (char inputChar)
	{
		if (!m_LetterInventory.IsLetterOwned (inputChar)) {
			inputChar = '\0';
		}
		return inputChar;
	}

	void AcceptStringInput (string userInput)
	{
		userInput = userInput.ToLower ();

		string[] words = userInput.Split (m_Separators);
		string command = "";
		if (userInput != "" && GameManager.instance.currentState != (int)GameFlowStates.ID.Dialogue) {
			for (int i = words.Length; i > 0; i--) {
				string word = words [i - 1];
				if (i < words.Length)
					word += " ";
				command = word + command;
				if (TextToInputMap.instance.FireCommand (command, m_CommandModifierManager.GetModifiers (words, i - 1)))
					break;
			}
		}

		ResetInput ();
	}

	void ResetInput ()
	{
		m_InputField.ActivateInputField ();
		m_InputField.text = null;
	}

}
