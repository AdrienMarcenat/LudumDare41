using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using System;

public class LetterInventory : MonoBehaviour
{
	private List<char> m_Letters;

	void Start ()
	{
		m_Letters = new List<char> () { 'l', 'e', 'f', 't', 'r', 'i', 'g', 'h' };
	}

	public bool IsLetterOwned (char letter)
	{
		return m_Letters.Contains (letter);
	}
}

