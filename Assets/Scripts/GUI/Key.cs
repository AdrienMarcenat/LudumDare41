using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour {

	private int m_x;
	public int x
	{
		get
		{
			return m_x;
		}
		set
		{
			m_x = value;
			SetPosition ();
		}
	}
	private int m_y;
	public int y
	{
		get
		{
			return m_y;
		}
		set
		{
			m_y = value;
			SetPosition ();
		}
	}
	public Text text;
	private char m_character;
	public char character
	{
		get
		{
			return m_character;
		}
		set
		{
			m_character = value;
			text.text = value.ToString();
		}
	}

	public void SetPosition(int _x = -1, int _y = -1)
	{
		if (_x>-1)
		{
			m_x = _x;
		}
		if (_y>-1)
		{
			m_y = _y;
		}
		RectTransform t = (RectTransform)transform;
		transform.localPosition = new Vector2 (15 + 65 * m_x, 15 + 65 * m_y);
	}
}