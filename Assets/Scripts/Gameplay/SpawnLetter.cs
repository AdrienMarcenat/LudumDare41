using UnityEngine;
using System.Collections;

public class SpawnLetter : MonoBehaviour
{
	[SerializeField] GameObject m_LetterPrefab;

	void OnDisable ()
	{
		Instantiate (m_LetterPrefab);
	}
}

