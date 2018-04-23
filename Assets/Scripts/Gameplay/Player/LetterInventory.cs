using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using System;
using UnityEngine.Analytics;

public class LetterInventory : MonoBehaviour
{
	private GUI m_Gui;
	private List<char> m_Letters;
	private List<char> m_LettersTemp;
	private List<char> m_InitialLetters;
	public static List<char> ms_AllLetters = new List<char> () {
		'a',
		'b',
		'c',
		'd',
		'e',
		'f',
		'g',
		'h',
		'i',
		'j',
		'k',
		'l',
		'm',
		'n',
		'o',
		'p',
		'q',
		'r',
		's',
		't',
		'u',
		'v',
		'w',
		'x',
		'y',
		'z',
		' ',
		'1',
		'2',
		'3',
		'4',
		'5',
		'6',
		'7',
		'8',
		'9',
		'0'
	};

	void Start ()
	{
		m_Gui = GameObject.Find ("GUI").GetComponent<GUI> ();
		/*m_Letters = new List<char> () {
			'l',
			'e',
			'f',
			't',
			'i',
			'g',
			'h',
			'2',
			'3',
			's',
			'a',
			'y',
			'x',
			'n',
			'v',
			'p',
			'd',
			'w'
		};*/
		m_Letters = ms_AllLetters;
		m_LettersTemp = new List<char> (m_Letters);
		m_InitialLetters = new List<char> (m_Letters);
		m_Gui.keyPanel.SetKeysAvailable (m_Letters);
	}

	void OnEnable ()
	{
		GameFlowEndLevelState.EndLevel += EndLevel;
	}

	void OnDisnable ()
	{
		GameFlowEndLevelState.EndLevel -= EndLevel;
	}

	void EndLevel (bool enter)
	{
		if (enter) {
			m_InitialLetters = new List<char> (m_Letters);
		} 
	}

	public bool IsLetterOwned (char letter)
	{
		return m_Letters.Contains (letter);
	}

	public void AddLetter (char letter)
	{
		if (!m_Letters.Contains (letter)) {
			m_Letters.Add (letter);
			m_Gui.keyPanel.SetAvailable (letter, true);
		}
	}

	public void RemoveLetter (char letter)
	{
		m_Letters.Remove (letter);
		m_Gui.keyPanel.SetAvailable (letter, false);
	}

	public void GodMode ()
	{
		m_LettersTemp = new List<char> (m_Letters);
		m_Letters = new List<char> (ms_AllLetters);
		UpdatePanel ();
	}

	public void NormalMode ()
	{
		m_Letters = new List<char> (m_LettersTemp);
		UpdatePanel ();
	}

	private void UpdatePanel ()
	{
		if (m_Gui != null) {
			foreach (char letter in ms_AllLetters) {
				m_Gui.keyPanel.SetAvailable (letter, IsLetterOwned (letter));
			}
		}
	}

	public void Reset ()
	{
		if (m_InitialLetters != null) {
			m_Letters = new List<char> (m_InitialLetters);
			m_LettersTemp = new List<char> (m_InitialLetters);
		}
		UpdatePanel ();
	}
}

