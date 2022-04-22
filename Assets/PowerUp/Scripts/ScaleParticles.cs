using UnityEngine;

[ExecuteInEditMode]
public class ScaleParticles : MonoBehaviour
{
	private ParticleSystem _ps;

	private void Start()
	{
		_ps = GetComponent<ParticleSystem>();
	}

	private void Update()
	{
		var main = _ps.main;
		main.startSize = transform.lossyScale.magnitude;
	}
}