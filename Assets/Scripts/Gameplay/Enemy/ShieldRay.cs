using UnityEngine;
using System.Collections;

public class ShieldRay : MonoBehaviour
{
	[SerializeField] private GameObject m_Generator1;
	[SerializeField] private GameObject m_Generator2;

	void Update ()
	{
		if (m_Generator1 == null && m_Generator2 == null) {
			Destroy (gameObject);
		}
	}
}

