using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEditorInternal;

public class Enemy : MonoBehaviour
{
	[SerializeField] protected int m_Type;
	[SerializeField] protected float m_PlayerDamageOnCollision;
	[SerializeField] protected AudioClip m_Sound;
	[SerializeField] protected float m_HitColorTime;

	protected Transform m_Target;
	protected Health m_Health;
	protected Animator m_Animator;
	protected SpriteRenderer m_Sprite;
	protected Color m_InitialColor;

	protected void Awake ()
	{
		ShipBuilder builder = GetComponent<ShipBuilder> ();
		if (builder) {
			builder.Build ();
		}
		m_Animator = GetComponentInChildren<Animator> ();
		m_Health = GetComponent<Health> ();
		m_Sprite = GetComponentInChildren<SpriteRenderer> ();
		m_InitialColor = m_Sprite.color;
	}

	void OnEnable ()
	{
		m_Health.Damage += Damage;
		m_Health.GameOver += GameOver;
	}

	void OnDisable ()
	{
		m_Health.Damage -= Damage;
		m_Health.GameOver -= GameOver;
	}

	private void Damage (float damage)
	{
		StartCoroutine (HitRoutine (damage));
	}

	IEnumerator HitRoutine (float damage)
	{
		m_Sprite.color = Color.Lerp (Color.blue, Color.red, damage);
		yield return new WaitForSeconds (m_HitColorTime);
		m_Sprite.color = m_InitialColor;
	}

	private void GameOver ()
	{
		StopAllCoroutines ();
		m_Sprite.color = m_InitialColor;
		m_Animator.SetBool ("IsDead", true);
		GetComponent<BoxCollider2D> ().enabled = false;
		SpawnLetter spawnLetter = GetComponent<SpawnLetter> ();
		if (spawnLetter != null)
			spawnLetter.Spawn ();
		Destroy (gameObject, 1);
	}

	private void OnTriggerStay2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player") {
			Health playerHealth = other.gameObject.GetComponent<Health> ();
			playerHealth.LoseHealth (m_PlayerDamageOnCollision);
		}
	}

	public void SetDamageOnCollision (float damage)
	{
		m_PlayerDamageOnCollision = damage;
	}

	public void SetHitColorTime (float time)
	{
		m_HitColorTime = time;
	}

	public void SetHitSound (AudioClip sound)
	{
		m_Sound = sound;
	}

	public void SetTarget (Transform target)
	{
		m_Target = target;
	}
}