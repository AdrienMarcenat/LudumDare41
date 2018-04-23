using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
	public bool done = true;
	public bool isWaiting = false;
	protected bool started = false;
	protected float m_startTime;
	protected float m_currentTime;

	public List<LevelSequence> m_orderSequences;
	protected int m_sequenceId;
	protected int m_orderId;

	[SerializeField] private DialogManager m_DialogueManager;

	void Awake ()
	{
		Reset ();
	}

	void Start ()
	{
		this.ParseFile ("Assets/Levels/Level01_Tutorial.lvl");
	}

	public void Reset ()
	{
		m_sequenceId = 0;
		m_orderId = 0;
	}

	protected float latestTime;

	void Update ()
	{
		if (isWaiting)
			return;
		
		m_currentTime += Time.deltaTime;
		while (m_sequenceId < m_orderSequences.Count) {
			LevelSequence currentSequence = m_orderSequences [m_sequenceId];
			if (!started) {
				if (currentSequence.start == LevelSequence.LevelSequenceStart.WHEN_DONE) {
					if (!done) {
						return;
					}
					m_currentTime = Time.deltaTime;
				} else {
					m_currentTime -= latestTime;
					latestTime = 0;
				}
				started = true;
			}
			while (m_orderId < currentSequence.orders.Count) {
				LevelOrder currentOrder = currentSequence.orders [m_orderId];
				if (currentOrder.when > m_currentTime) {
					return;
				}
				latestTime = currentOrder.when;
				ExecuteOrder (currentOrder);
				m_orderId++;
			}
			started = false;
			m_sequenceId++;
		}
	}

	private void ExecuteOrder (LevelOrder order)
	{
		//Debug.Log(order);
		switch (order.orderType) {
		case(LevelOrderType.SPAWN):
			SpawnLevelOrder spawnOrder = (SpawnLevelOrder)order;
			for (int i = 0; i < spawnOrder.entities.Count; i++) {
				for (int k = 0; k < spawnOrder.entitiesQuantity [i]; k++) {
					Instantiate ((spawnOrder.entities [i]));
				}
			}
			break;
		case(LevelOrderType.MUSIC):
			Debug.Log ("MUSIC!!!");
			break;
		case(LevelOrderType.TALK):
			TalkLevelOrder talkLevelOrder = (TalkLevelOrder)order;
			m_DialogueManager.TriggerDialogue (talkLevelOrder.tag);
			break;
		case(LevelOrderType.END_LEVEL):
			Debug.Log ("END LEVEL!!!");
			break;
		case(LevelOrderType.WAIT_TRIGGER):
			isWaiting = true;
			Debug.Log ("WAITING TRIGGER");
			break;
		case(LevelOrderType.WAIT_DIALOGUE):
			isWaiting = true;
			EventManager.TriggerEvent ("WaitDialogue", new CommandModifier (1, 1, 1));
			WaitDialogueLevelOrder waitDialogueLevelOrder = (WaitDialogueLevelOrder)order;
			m_DialogueManager.TriggerDialogue (waitDialogueLevelOrder.tag);
			break;
		}
	}
}
