using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	[SerializeField] int type;
	[SerializeField] int totalAmmo = int.MaxValue;
	[SerializeField] int currentAmmo;
	[SerializeField] float ammoVelocity;
	[SerializeField] float m_FireRate;

	[SerializeField] GameObject bulletPrefab;
	[SerializeField] AudioClip fireSound;

	private List<float> m_FireCommands;
	private float m_FireDelay;
	private float m_FireCommandNumber;

	void Start ()
	{
		m_FireCommands = new List<float> ();
		currentAmmo = totalAmmo;
		m_FireDelay = m_FireRate;
	}

	void Update ()
	{
		if (m_FireDelay < m_FireRate)
		{
			m_FireDelay += Time.deltaTime;
		}
		else
		if (m_FireCommands.Count > 0)
		{
			FireInternal ();
		}
	}

	public void SetAmmo (int amount)
	{
		currentAmmo += amount;
		currentAmmo = Mathf.Clamp (currentAmmo, 0, totalAmmo);
	}

	public void Fire (float numberModifier, float sizeModifier)
	{
		for (int i = 0; i < numberModifier; i++)
		{
			m_FireCommands.Add (sizeModifier);
		}
	}

	private void FireInternal ()
	{
		if (currentAmmo == 0)
		{
			return;
		}

		m_FireDelay = 0;
		SetAmmo (-1);
		SoundManager.PlayMultiple (fireSound);

		// The bullet is a child of the current room so it will be deactivated when the player leave the room
		GameObject bullet = Instantiate (bulletPrefab);
		bullet.transform.position = transform.position;
		bullet.transform.localScale *= m_FireCommands [0];
		bullet.GetComponent<Rigidbody2D> ().velocity = ammoVelocity * Vector3.up;

		m_FireCommands.RemoveAt (0);
	}

	public void Reload ()
	{
		currentAmmo = totalAmmo;
	}

	public int GetWeaponType ()
	{
		return type;
	}

	public int GetAmmo ()
	{
		return currentAmmo;
	}
}
