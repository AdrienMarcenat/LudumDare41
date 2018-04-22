using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

	protected float m_startTime;
	protected float m_currentTime;

	public List<LevelSequence> m_orderSequences;
	protected int m_sequenceId;
	protected int m_orderId;

	void Awake ()
	{
		Reset ();
		this.ParseFile ("Assets/Datas/TestLevel.txt");
	}

	public void Reset ()
	{
		m_sequenceId = 0;
		m_orderId = 0;
	}

	void Update ()
	{
		m_currentTime += Time.deltaTime;
		if (m_sequenceId >= m_orderSequences.Count)
		{
			return;
		}
		LevelSequence currentSequence = m_orderSequences [m_sequenceId];
		float latestTime;
		while (m_orderId < currentSequence.orders.Count)
		{
			LevelOrder currentOrder = currentSequence.orders [m_orderId];
			if (currentOrder.when > m_currentTime)
			{
				break;
			}
			latestTime = currentOrder.when;
			ExecuteOrder (currentOrder);
			m_orderId++;
		}
	}

	private void ExecuteOrder (LevelOrder order)
	{
		//Debug.Log(order);
		switch (order.orderType)
		{
		case(LevelOrderType.SPAWN):
			SpawnLevelOrder spawnOrder = (SpawnLevelOrder)order;
			for (int i = 0; i < spawnOrder.entities.Count; i++)
			{
				for (int k = 0; k < spawnOrder.entitiesQuantity [i]; k++)
				{
					Instantiate ((spawnOrder.entities [i]));
				}
			}
			break;
		case(LevelOrderType.MUSIC):
			Debug.Log ("MUSIC!!!");
			break;
		case(LevelOrderType.TALK):
			TalkLevelOrder talkLevelOrder = (TalkLevelOrder)order;
			DialogueManager.instance.TriggerDialogue (talkLevelOrder.tag);
			break;
		case(LevelOrderType.END_LEVEL):
			Debug.Log ("END LEVEL!!!");
			break;
		}
	}
}
