using UnityEngine;
using System.Collections;

public class PlayerSoundManager : MonoBehaviour
{
	private PlayerEventManager playerEventManager;
	private Health playerHealth;

	[SerializeField] AudioClip damageSound;
	[SerializeField] AudioClip healSound;

	void Awake ()
	{
		playerEventManager = GetComponent<PlayerEventManager> ();
		playerHealth = GetComponent<Health> ();
	}

	void OnEnable ()
	{
		playerHealth.SimpleDamage += Damage;
		PlayerEventManager.Heal += Heal;
		PlayerEventManager.LetterPicked += Heal;
	}

	void OnDisable ()
	{
		playerHealth.SimpleDamage -= Damage;
		PlayerEventManager.Heal -= Heal;
		PlayerEventManager.LetterPicked -= Heal;
	}

	private void Heal ()
	{
		SoundManager.PlayMultiple (healSound);
	}

	private void Damage ()
	{
		SoundManager.PlayMultiple (damageSound);
	}
}

