using System.Threading.Tasks;
using MyProject.Events;
using UnityEngine;

public class AutomaticRifle : PlayerWeapon
{
	public override int Type => PlayerWeapon.AUTOMATIC_RIFLE;

	protected override async void Fire(PlayerInputMessage message)
	{
		if (Time.time - Reload < LastTime)
		{
			return;
		}

		if (!message.Fire)
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