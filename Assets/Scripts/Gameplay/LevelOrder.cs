using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSequence
{

	public enum LevelSequenceStart
	{
		NOW,
		WHEN_DONE,
		ERROR
	}

	public LevelSequenceStart start = LevelSequenceStart.ERROR;
	public List<LevelOrder> orders = new List<LevelOrder> ();

	static public LevelSequenceStart Parse (string type)
	{
		switch (type.ToLower ())
		{
		case("[now]"):
			return LevelSequenceStart.NOW;
		case("[done]"):
			return LevelSequenceStart.WHEN_DONE;
		}
		return LevelSequenceStart.ERROR;
	}
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

	public LevelOrder ()
	{
		when = 0;
	}

	public LevelOrder (float time)
	{
		when = time;
	}

	public override string ToString ()
	{
		return orderType.ToString () + ":" + when.ToString ();
	}
}

public class SpawnLevelOrder : LevelOrder
{
	public List<GameObject> entities = new List<GameObject> ();
	public List<int> entitiesQuantity = new List<int> ();

	public SpawnLevelOrder (string[] args, float time) : base (time)
	{
		orderType = LevelOrderType.SPAWN;
		for (int k = 0; k < args.Length; k++)
		{
			string[] things = args [k].Split ('*');
			string prefab;
			int num = 1;
			if (things.Length > 1)
			{
				num = int.Parse (things [0]);
				prefab = things [1];
			}
			else
			{
				prefab = things [0];
			}
			GameObject entity = RessourceManager.instance.LoadPrefab (prefab);
			entity = (GameObject)entity;
			entities.Add (entity);
			entitiesQuantity.Add (num);
		}
	}
}

public class MusicLevelOrder : LevelOrder
{
	public string music;
	//TODO
	public MusicLevelOrder ()
	{
		orderType = LevelOrderType.MUSIC;
	}
}

public class TalkLevelOrder : LevelOrder
{
	public string tag;

	public TalkLevelOrder (string tag, float time) : base (time)
	{
		this.tag = tag;
		orderType = LevelOrderType.TALK;
	}
}

public class EndLevelOrder : LevelOrder
{
	public EndLevelOrder ()
	{
		orderType = LevelOrderType.END_LEVEL;
	}
}