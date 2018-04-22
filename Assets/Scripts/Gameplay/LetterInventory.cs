using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using System;
using UnityEngine.Analytics;

public class LetterInventory : MonoBehaviour
{
	private List<char> m_Letters;
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
		//m_Letters = new List<char> () { 'l', 'e', 'f', 't', 'r', 'i', 'g', 'h' };
		m_Letters = ms_AllLetters;
	}

	public bool IsLetterOwned (char letter)
	{
		return m_Letters.Contains (letter);
	}

	public void AddLetter (char letter)
	{
		m_Letters.Add (letter);
	}

	public void RemoveLetter (char letter)
	{

		m_Letters.Remove (letter);
	}
}

