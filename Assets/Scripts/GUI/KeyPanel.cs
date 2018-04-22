using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPanel : MonoBehaviour
{
	public static string ALL_LETTERS = "abcdefghijklmnopqrstuvwxyz 0123456789";

	public GameObject keyPrefab;
	protected Dictionary<char,Key> m_keyHolders = new Dictionary<char, Key> ();
	protected Dictionary<char,bool> m_keysAvailable = new Dictionary<char, bool> ();

	protected Key Create (char keyType)
	{
		Key newKey = Instantiate (keyPrefab, transform).GetComponent<Key> ();
		newKey.character = keyType;
		return newKey;
	}

	protected Vector2 IdToPosition (int i)
	{
		int x = i % 7;
		int y = i / 7;
		return new Vector2 (x * 34, -44 - y * 34);
	}

	void Start ()
	{
		int i = 0;
		foreach (char c in ALL_LETTERS) {
			Key k = Create (c);
			k.transform.localPosition = IdToPosition (i);
			k.name = "Key_" + c;
			m_keyHolders.Add (c, k);
			m_keysAvailable.Add (c, false);
			k.gameObject.SetActive (false);
			i++;
		}
	}

	public void SetKeysAvailable (Dictionary<char,bool> keysAvailable)
	{
		foreach (char c in ALL_LETTERS) {
			SetAvailable (c, keysAvailable [c]);
		}
	}

	public void SetKeysAvailable (ICollection<char> keysAvailable)
	{
		foreach (char c in ALL_LETTERS) {
			SetAvailable (c, keysAvailable.Contains (c));
		}
	}

	public void SetKeysAvailable (string keysAvailable)
	{
		foreach (char c in ALL_LETTERS) {
			SetAvailable (c, keysAvailable.Contains (c.ToString ()));
		}
	}

	public void SetAvailable (char c, bool b = true)
	{
		bool currently = m_keysAvailable [c];
		if (b != currently) {
			m_keysAvailable [c] = b;
			m_keyHolders [c].gameObject.SetActive (b);
		}
	}
}
