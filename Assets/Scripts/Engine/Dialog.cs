﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Dialog
{
	public struct Sentence
	{
		public string name;
		public string sentence;

		public Sentence (string name, string sentence)
		{
			this.name = name;
			this.sentence = sentence;
		}
	}

	public List<Sentence> sentences;

	public Dialog ()
	{
		sentences = new List<Sentence> ();
	}
}

