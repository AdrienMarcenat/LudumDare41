using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	[SerializeField] int type;
	[SerializeField] int totalAmmo = int.MaxValue;
	[SerializeField] int currentAmmo;
	[SerializeField] float ammoVelocity;

	[SerializeField] GameObject bulletPrefab;
	[SerializeField] AudioClip fireSound;

	void Start ()
	{
		currentAmmo = totalAmmo;
	}

	public void SetAmmo (int amount)
	{
		currentAmmo += amount;
		currentAmmo = Mathf.Clamp (currentAmmo, 0, totalAmmo);
	}

	public bool Fire ()
	{
		SetAmmo (-1);
		SoundManager.PlayMultiple (fireSound);

		// The bullet is a child of the current room so it will be deactivated when the player leave the room
		GameObject bullet = Instantiate (bulletPrefab);
		bullet.transform.position = transform.position;
		bullet.GetComponent<Rigidbody2D> ().velocity = ammoVelocity * Vector3.up;

		return true;
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
