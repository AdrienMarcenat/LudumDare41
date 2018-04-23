using UnityEngine;
using System.Collections;

public class PlayerSoundManager : MonoBehaviour
{
	private PlayerEventManager playerEventManager;
	private Health playerHealth;

	[SerializeField] AudioClip damageSound;
	[SerializeField] AudioClip healSound;
	[SerializeField] AudioClip gameOverSound;

	void Awake ()
	{
		playerEventManager = GetComponent<PlayerEventManager> ();
		playerHealth = GetComponent<Health> ();
	}

	void OnEnable ()
	{
		playerHealth.SimpleDamage += Damage;
		playerHealth.GameOver += GameOver;
		playerEventManager.Heal += Heal;
		playerEventManager.LetterPicked += Heal;
	}

	void OnDisable ()
	{
		playerHealth.SimpleDamage -= Damage;
		playerHealth.GameOver -= GameOver;
		playerEventManager.Heal -= Heal;
		playerEventManager.LetterPicked -= Heal;
	}

	private void Heal ()
	{
		SoundManager.PlayMultiple (healSound);
	}

	private void Damage ()
	{
		SoundManager.PlayMultiple (damageSound);
	}

	private void GameOver ()
	{
		SoundManager.PlayMultiple (gameOverSound);
	}
}

