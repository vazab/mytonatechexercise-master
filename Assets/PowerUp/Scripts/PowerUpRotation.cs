using UnityEngine;

public class PowerUpRotation : MonoBehaviour
{
	private float _rotationSpeed = 99f;
	private bool _reverse = false;

	private void Update()
	{
		if (_reverse)
			//transform.Rotate(Vector3.back * Time.deltaTime * this.rotationSpeed);
			transform.Rotate(new Vector3(0f,0f,1f) * Time.deltaTime * this._rotationSpeed);
		else
			//transform.Rotate(Vector3.forward * Time.deltaTime * this.rotationSpeed);
			transform.Rotate(new Vector3(0f,0f,1f) * Time.deltaTime * this._rotationSpeed);
	}

	public void SetRotationSpeed(float speed)
	{
		_rotationSpeed = speed;
	}

	public void SetReverse(bool reverse)
	{
		_reverse = reverse;
	}
}
