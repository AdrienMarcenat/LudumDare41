using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour
{
	private List<Weapon> m_Weapons;

	void Start ()
	{
		m_Weapons = new List<Weapon> ();
		foreach (Weapon w in GetComponents<Weapon> ())
		{
			m_Weapons.Add (w);
		}
		m_Weapons.Sort (delegate(Weapon a, Weapon b)
		{
			return a.GetWeaponType () < b.GetWeaponType () ? 1 : 0;
		});
	}

	public void Fire (int weaponType, float numberModifier, float sizeModifier)
	{
		if (weaponType < m_Weapons.Count)
		{
			m_Weapons [weaponType].Fire (numberModifier, sizeModifier);
		}
		else
		{
			Debug.Log ("Wrong weapon Type");	
		}
	}
}

