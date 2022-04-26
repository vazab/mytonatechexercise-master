using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField] private bool _damagePlayer;
	[SerializeField] private bool _damageMob;
	
	public float Damage;
	private bool _destroyed = false;
	private float _speed = 8f;
	private float _timeToLive = 5f;
	private float timer = 0f;

	protected virtual void OnTriggerEnter(Collider other)
	{
		if (_destroyed)
		{
			return;
		}

		if (_damagePlayer && other.CompareTag("Player"))
		{
			other.GetComponent<Player>().TakeDamage(Damage);
			_destroyed = true;
		}

		if (_damageMob && other.CompareTag("Mob"))
		{
			other.GetComponent<Mob>().TakeDamage(Damage);
			_destroyed = true;
		}
	}

	protected virtual void Update()
	{
		if (_destroyed == false)
		{
			transform.position += transform.forward * _speed * Time.deltaTime;
		}

		timer += Time.deltaTime;
		
		if (timer > _timeToLive)
		{
			Destroy(gameObject);
		}
	}
}