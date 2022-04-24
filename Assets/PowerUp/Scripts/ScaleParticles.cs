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
		var psmain = _ps.main;
		psmain.startSize = transform.lossyScale.magnitude;
	}
}