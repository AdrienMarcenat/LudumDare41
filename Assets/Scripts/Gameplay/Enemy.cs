using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	[SerializeField] protected int type;
	[SerializeField] protected float playerDamage;
	[SerializeField] protected AudioClip sound;
	[SerializeField] protected float hitColorTime;

	protected Transform target;
	protected Health health;
	protected Animator animator;
	protected SpriteRenderer sprite;
	protected Color initialColor;

	public delegate void SimpleEvent ();

	protected void Awake ()
	{
		animator = GetComponent<Animator> ();
		health = GetComponent<Health> ();
		sprite = GetComponent<SpriteRenderer> ();
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		initialColor = sprite.color;
	}

	void OnEnable ()
	{
		health.Damage += Damage;
		health.GameOver += GameOver;
	}

	void OnDisable ()
	{
		health.Damage -= Damage;
		health.GameOver -= GameOver;
	}

	private void Damage (float damage, int weaponType)
	{
		StartCoroutine (HitRoutine (damage, weaponType));
	}

	IEnumerator HitRoutine (float damage, int weaponType)
	{
		sprite.color = Color.Lerp (Color.blue, Color.red, damage);
		yield return new WaitForSeconds (hitColorTime);
		sprite.color = initialColor;
	}

	private void GameOver ()
	{
		animator.SetTrigger ("isDying");
		GetComponent<BoxCollider2D> ().enabled = false;
		Destroy (gameObject, 1);
	}

	private void OnCollisionStay2D (Collision2D other)
	{
		if (other.gameObject.tag == "Player") {
			Health playerHealth = other.gameObject.GetComponent<Health> ();
			playerHealth.LoseHealth (playerDamage, 0);
		}
	}
}