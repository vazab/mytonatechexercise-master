using System.Threading.Tasks;
using MyProject.Events;
using UnityEngine;

public class Rifle : PlayerWeapon
{	
	public override int Type => PlayerWeapon.RIFLE;

	protected override void Awake()
	{
		base.Awake();
		EventBus<PlayerInputMessage>.Sub(Fire);
	}

	protected override async void Fire(PlayerInputMessage message)
	{
		if (Time.time - Reload < LastTime)
		{	
			return;
		}

		if (message.Fire == false)
		{
			return;
		}
		
		LastTime = Time.time;
		GetComponent<PlayerAnimator>().TriggerShoot();

		await Task.Delay(16);

		var bullet = Instantiate(BulletPrefab, FirePoint.position, transform.rotation);
		bullet.Damage = GetDamage();
		Vfx.Play();
	}
}