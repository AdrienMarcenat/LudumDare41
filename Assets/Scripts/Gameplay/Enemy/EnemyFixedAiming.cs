using UnityEngine;
using System.Collections;

public class EnemyFixedAiming : EnemyAI
{
	protected override void Fire (int weaponType)
	{
		m_WeaponManager.Fire (weaponType, m_FireSalveNumber, m_SizeModifier, m_ShootDirection.localPosition);
	}
}

