using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public static class LevelGeneratorParserer {

    public static void ParseFile(this LevelGenerator levelGenerator, string file)
    {
        string[] lines = File.ReadAllLines(file);
        List<LevelSequence> sequences = levelGenerator.m_orderSequences = new List<LevelSequence>();
        LevelSequence currentSequence = new LevelSequence();
        foreach (string line in lines)
        {
            try
            {
                string[] words = line.Split(' ');
                if(words.Length==0)
                {
                    continue;
                }
                LevelSequence.LevelSequenceStart type = MakeLevelSequence(words[0]);
                //New sequence
                if(type!=LevelSequence.LevelSequenceStart.ERROR)
                {
                    if(currentSequence.orders.Count>0 && currentSequence.start){
                        sequences.Add(currentSequence);
                    }
                    currentSequence = new LevelSequence();
                    currentSequence.start = type;
                }
                //New order
                LevelOrder order = MakeLevelOrder(words);

            }
            catch
            {
                Debug.LogWarning("Couldn't read line \"" + line + "\" of file \"" + file + "\"");
            }
        }
    }

    public static T[] SubArray<T>(this T[] data, int index, int length=-1)
    {
        if (length == -1)
        {
            length = data.Length - index;
        }
        T[] result = new T[length];
        Array.Copy(data, index, result, 0, length);
        return result;
    }

    static protected LevelSequence.LevelSequenceStart MakeLevelSequence(string type)
    {
        return LevelSequence.LevelSequenceStart.ERROR;
    }

    static protected LevelOrder MakeLevelOrder(string[] args){
        System.DateTime time = System.DateTime.Parse(args[0], "mm:ss.fff");
        float floatTime = time.Millisecond;
        switch (args[1].ToLower())
        {
            case("spawn"):
                return new SpawnLevelOrder(args.SubArray(2));

            case("talk"):
                //TODO
                break;

            case("music"):
                //TODO
                break;

            case("endgame"):
                //TODO
                break;
        }
        return null;
    }
        
    static public GameObject GetPrefab(string name){
        return (GameObject)Resources.Load("Prefabs/"+name, typeof(GameObject));
    }
}
