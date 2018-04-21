using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using System;
using UnityEngine.Analytics;

public class LetterInventory : MonoBehaviour
{
	private List<char> m_Letters;

	void Start ()
	{
		//m_Letters = new List<char> () { 'l', 'e', 'f', 't', 'r', 'i', 'g', 'h' };
		m_Letters = new List<char> () {
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
			'0',
			'1',
			'2'
		};
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

