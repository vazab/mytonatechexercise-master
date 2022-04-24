using UnityEngine;

public class TakeablePowerUp : MonoBehaviour
{
	private CustomizablePowerUp _customPowerUp;

	private void Start()
	{
		_customPowerUp = (CustomizablePowerUp)transform.parent.gameObject.GetComponent<CustomizablePowerUp>();
		//this.audio.clip = customPowerUp.pickUpSound;
	}

	private void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "Player")
		{
			PowerUpManager.Instance.Add(_customPowerUp);
			
			if (_customPowerUp.pickUpSound != null)
			{
				AudioSource.PlayClipAtPoint(_customPowerUp.pickUpSound, transform.position);
			}
			
			Destroy(transform.parent.gameObject);
		}
	}
}
