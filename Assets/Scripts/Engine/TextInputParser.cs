using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextInputParser : MonoBehaviour
{
	[SerializeField] private InputField m_InputField;
	[SerializeField] private LetterInventory m_LetterInventory;

	void Awake ()
	{
		m_InputField.onEndEdit.AddListener (AcceptStringInput);
	}

	void Start ()
	{
		m_InputField.onValidateInput += delegate(string input, int charIndex, char addedChar)
		{
			return FilterChar (addedChar);
		};
		m_InputField.Select ();
		m_InputField.ActivateInputField ();
	}

	private char FilterChar (char inputChar)
	{
		if (!m_LetterInventory.IsLetterOwned (inputChar))
		{
			inputChar = '\0';
		}
		return inputChar;
	}

	void AcceptStringInput (string userInput)
	{
		userInput = userInput.ToLower ();

		TextToInputMap.instance.FireCommand (userInput);

		ResetInput ();
	}

	void ResetInput ()
	{
		m_InputField.ActivateInputField ();
		m_InputField.text = null;
	}

}
