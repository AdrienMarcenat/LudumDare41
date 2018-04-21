using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandBar : MonoBehaviour 
{

	public Text commandText;

	public void SetText(string text)
	{
		commandText.text = text;
	}
}
