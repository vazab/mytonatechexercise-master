using System.Threading.Tasks;
using MyProject.Events;
using UnityEngine;

// Bonus!
// Refactror Rifle, AutomaticRifle and Shotgun classes as you want
public class Shotgun : PlayerWeapon
{
	public override int Type => PlayerWeapon.SHOTGUN;

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
		var directions = SpreadDirections(transform.rotation.eulerAngles, 3, 20);
		
		foreach (var direction in directions)
		{
			var bullet = Instantiate(BulletPrefab, FirePoint.position, Quaternion.Euler(direction));
			bullet.Damage = GetDamage();
		}

		Vfx.Play();
	}

	public Vector3[] SpreadDirections(Vector3 direction, int num, int spreadAngle)
	{
		Vector3[] result = new Vector3[num];
		result[0] = new Vector3(0, direction.y - (num - 1) * spreadAngle / 2, 0);
		
		for (int i = 1; i < num; i++)
		{
			result[i] = result[i - 1] + new Vector3(0, spreadAngle, 0);
		}

		return result;
	}
}