using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogGUI : MonoBehaviour
{

	private Coroutine m_currentHeightOrder;
	private RectTransform rect;

	void Awake ()
	{
		rect = (RectTransform)transform;
	}

	void Start ()
	{
		
	}

	void OnDisable ()
	{
		StopAllCoroutines ();
	}

	public void Close (float time = 0.3f)
	{
		ChangeHeight (-20);
	}

	public void ChangeHeight (float destination, float time = 0.4f)
	{
		if (m_currentHeightOrder != null) {
			StopCoroutine (m_currentHeightOrder);
		}
		m_currentHeightOrder = StartCoroutine (ChangeHeightCoroutine (destination));
	}

	float GetHeight ()
	{
		return rect.sizeDelta.y;
	}

	void SetHeight (float height)
	{
		rect.sizeDelta = new Vector2 (rect.sizeDelta.x, (int)height);
	}

	IEnumerator ChangeHeightCoroutine (float destination, float time = 0.2f)
	{
		float initialHeight = GetHeight ();
		int Smoothness = (int)(time / 0.01);
		for (int k = 0; k < Smoothness; k++) {
			float alpha = ((float)k) / Smoothness;
			SetHeight (alpha * destination + initialHeight * (1 - alpha));
			yield return new WaitForSeconds (time / Smoothness);
		}
		SetHeight (destination);
	}

	public void Open (int n = 1)
	{
		ChangeHeight (10 + n * 22);
	}

	public void QuickOpen (int n = 1)
	{
		if (GetHeight () < 0) {
			SetHeight (0);
		}
		ChangeHeight (10f + n * 22, 0.1f);
	}
}
