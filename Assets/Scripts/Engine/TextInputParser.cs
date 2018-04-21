using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextInputParser : MonoBehaviour
{
	[SerializeField] private InputField m_InputField;

	void Awake ()
	{
		m_InputField.onEndEdit.AddListener (AcceptStringInput);
	}

	void Start ()
	{
		m_InputField.Select ();
		m_InputField.ActivateInputField ();
	}

	void AcceptStringInput (string userInput)
	{
		userInput = userInput.ToLower ();
		Debug.Log (userInput);

		TextToInputMap.instance.FireCommand (userInput);

		ResetInput ();
	}

	void ResetInput ()
	{
		m_InputField.ActivateInputField ();
		m_InputField.text = null;
	}

}
