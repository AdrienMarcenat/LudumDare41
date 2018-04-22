using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private float damage;
	[SerializeField] private float range;
	[SerializeField] private float penetration;
	[SerializeField] private int weaponType;
	[SerializeField] private string targetTag;
	[SerializeField] private bool m_FollowShooter;

	void Update ()
	{
		range -= Time.deltaTime;
		if (range < 0)
			Destroy (gameObject);
	}

	private void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == targetTag)
		{
			Health targetHealth = other.GetComponent<Health> ();
			if (targetHealth)
			{
				targetHealth.LoseHealth (damage);
			}

			Destroy (gameObject, penetration);
		}
	}

	void OnDisable ()
	{
		Destroy (this.gameObject);
	}

	public bool IsFollowingShooter ()
	{
		return m_FollowShooter;
	}
}
