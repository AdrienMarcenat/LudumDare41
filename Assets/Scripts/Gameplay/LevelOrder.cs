using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct LevelSequence
{

	public enum LevelSequenceStart
	{
		NOW,
		WHEN_DONE,
        ERROR
	}

	public LevelSequenceStart start = LevelSequenceStart.ERROR;
    public List<LevelOrder> orders = new List<LevelOrder>();

}

public enum LevelOrderType
{
	SPAWN,
	MUSIC,
    TALK,
	END_LEVEL
}

public abstract class LevelOrder
{
	public LevelOrderType orderType
	{
		get;
		protected set;
	}
	public float when;

	public string ToString(){
		return orderType.ToString () + ":" + when.ToString ();
	}
}

public class SpawnLevelOrder : LevelOrder
{
	public LevelOrderType orderType = LevelOrderType.SPAWN;
	public List<GameObject> entities;
    public List<int> entitiesQuantity;

    public SpawnLevelOrder(string[] args)
    {
        for (int k = 0; k < args.Length; k++)
        {
            string[] things = args[k].Split("*");
            string prefab;
            int num = 1;
            if (things.Length > 1)
            {
                num = (int)things[0];
                prefab = things[1];
            }
            else
            {
                prefab = things[0];
            }
            entities.Add(LevelGeneratorParserer.GetPrefab(prefab));
            entitiesQuantity.Add(num);
        }
    }
}

public class MusicLevelOrder : LevelOrder
{
	public LevelOrderType orderType = LevelOrderType.MUSIC;
	public string music; //TODO
}

public class TalkLevelOrder : LevelOrder
{
    public LevelOrderType orderType = LevelOrderType.MUSIC;
    public string face; //TODO
    public string text; //TODO
}

public class EndLevelOrder : LevelOrder
{
	public LevelOrderType orderType = LevelOrderType.END_LEVEL;
}