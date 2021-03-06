﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class CommandModifierDictionnary
{
	private Dictionary<string, float> m_Modifiers;

	public void FillModifier (string filename)
	{
		m_Modifiers = new Dictionary<string, float> ();

		char[] separators = { ':' };
		#if UNITY_EDITOR
		filename = "Assets/" + filename;
		#endif

		string[] lines = File.ReadAllLines (filename);

		for (int i = 0; i < lines.Length; i++)
		{
			string[] datas = lines [i].Split (separators);

			// If there is an error in print a debug message
			if (datas.Length != 2)
			{
				Debug.Log ("Invalid number of data line " + i + " expecting 2, got " + datas.Length);
				return;
			}

			string modifier = datas [0].ToLower ();
			float value = 0;
			if (float.TryParse (datas [1], out value))
			{
				AddEntry (modifier, value);
			}
		}
	}

	public void AddEntry (string modifier, float value)
	{
		m_Modifiers.Add (modifier, value);
	}

	public float GetModifier (string modifier)
	{
		float value = 0;
		m_Modifiers.TryGetValue (modifier, out value);
		return value;
	}

}

