using UnityEngine;
using System.Collections;

public class SpawnLetter : MonoBehaviour
{
	[SerializeField] GameObject m_LetterPrefab;

	public void Spawn ()
	{
		LevelGenerator.instance.SpawnObject (m_LetterPrefab);
	}
}

