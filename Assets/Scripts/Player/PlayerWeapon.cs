using MyProject.Events;
using UnityEngine;

public abstract class PlayerWeapon : MonoBehaviour
{
	public const int RIFLE = 0;
	public const int SHOTGUN = 1;
	public const int AUTOMATIC_RIFLE = 2;
	public abstract int Type { get; }
	public GameObject Model;

	protected virtual void Awake()
	{
		GetComponent<Player>().WeaponChanged += Change;
	}

	protected virtual void OnDestroy()
	{
		EventBus<PlayerInputMessage>.Unsub(Fire);
	}

	protected void Change(int type)
	{
		EventBus<PlayerInputMessage>.Unsub(Fire);
		if (type == Type)
		{
			EventBus<PlayerInputMessage>.Sub(Fire);
		}

		Model.SetActive(type == Type);
	}

	protected abstract void Fire(PlayerInputMessage message);
}