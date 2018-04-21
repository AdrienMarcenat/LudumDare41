using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPanel : MonoBehaviour
{
	public GameObject keyPrefab;
	protected Dictionary<char,Key> listOfKeys;
	protected List<char> orderedKeys;

	public Key Create(char keyType)
	{
		Key newKey = Instantiate (keyPrefab, transform).GetComponent<Key>();
		newKey.character = keyType;
		return newKey;
	}

	public void Add(char key)
	{

	}

	public void Remove(char key)
	{

	}
}
