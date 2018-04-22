using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public static class LevelGeneratorParserer
{

	public static void ParseFile (this LevelGenerator levelGenerator, string file)
	{
		string[] lines = File.ReadAllLines (file);
		List<LevelSequence> sequences = levelGenerator.m_orderSequences = new List<LevelSequence> ();
		LevelSequence currentSequence = new LevelSequence ();
		foreach (string line in lines)
		{
			try
			{
				string[] words = line.Split (' ');
				if (words.Length == 0)
				{
					continue;
				}
				LevelSequence.LevelSequenceStart type = LevelSequence.Parse (words [0]);
				//New sequence
				if (type != LevelSequence.LevelSequenceStart.ERROR)
				{
					if (currentSequence.orders.Count > 0 && currentSequence.start != LevelSequence.LevelSequenceStart.ERROR)
					{
						sequences.Add (currentSequence);
					}
					currentSequence = new LevelSequence ();
					currentSequence.start = type;
					continue;
				}
				//New order
				LevelOrder order = MakeLevelOrder (words);
				currentSequence.orders.Add (order);

			}
			catch (Exception e)
			{
				Debug.LogWarning ("Couldn't read line \"" + line + "\" of file \"" + file + "\"");
				Debug.LogWarning (e.Message);
			}
		}
		if (currentSequence.orders.Count > 0 && currentSequence.start != LevelSequence.LevelSequenceStart.ERROR)
		{
			sequences.Add (currentSequence);
		}
	}

	public static T[] SubArray<T> (this T[] data, int index, int length = -1)
	{
		if (length == -1)
		{
			length = data.Length - index;
		}
		T[] result = new T[length];
		Array.Copy (data, index, result, 0, length);
		return result;
	}

	static private LevelOrder MakeLevelOrder (string[] args)
	{
		System.DateTime time = System.DateTime.ParseExact (args [0], "mm:ss.fff", null);
		float floatTime = time.Minute * 60 + time.Second + 0.001f * time.Millisecond;
		switch (args [1].ToLower ())
		{
		case("spawn"):
			return new SpawnLevelOrder (args.SubArray (2), floatTime);

		case("talk"):
			return new TalkLevelOrder (args [2], floatTime);

		case("music"):
                //TODO
			break;

		case("endgame"):
                //TODO
			break;
		}
		return null;
	}
}
