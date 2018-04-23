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
		'9'
		//'0'
	};

	void Start ()
	{
		m_Gui = GameObject.Find ("GUI").GetComponent<GUI> ();
		//m_Letters = new List<char> () { 'l', 'e', 'f', 't', 'r', 'i', 'g', 'h' };
		m_Letters = ms_AllLetters;
		m_LettersTemp = m_Letters;
		m_Gui.keyPanel.SetKeysAvailable (m_Letters);
	}

	public bool IsLetterOwned (char letter)
	{
		return m_Letters.Contains (letter);
	}

	public void AddLetter (char letter)
	{
		m_Letters.Add (letter);
		m_Gui.keyPanel.SetAvailable (letter, true);
	}

	public void RemoveLetter (char letter)
	{
		m_Letters.Remove (letter);
		m_Gui.keyPanel.SetAvailable (letter, false);
	}

	public void GodMode ()
	{
		m_LettersTemp = m_Letters;
		m_Letters = ms_AllLetters;
		UpdatePanel ();
	}

	public void NormalMode ()
	{
		m_Letters = m_LettersTemp;
		UpdatePanel ();
	}

	private void UpdatePanel ()
	{
		foreach (char letter in ms_AllLetters) {
			m_Gui.keyPanel.SetAvailable (letter, IsLetterOwned (letter));
		}
	}
}

