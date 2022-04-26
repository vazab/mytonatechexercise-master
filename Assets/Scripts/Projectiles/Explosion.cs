using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _startSize = 1f;
    [SerializeField] private float _maxSize = 1f;
    [SerializeField] private float _duration = 1f;

    [HideInInspector] public float Damage;
    private Vector3 _startScale;
    private Vector3 _targetScale;
    private Vector3 _startRotation;
    private Vector3 _targetRotation;

    private void Start()
    {
        transform.localScale = new Vector3(_startSize, _startSize, _startSize);
        _startScale = transform.localScale;
        _targetScale = new Vector3(_maxSize, _maxSize, _maxSize);
        
        _startRotation = transform.eulerAngles;
        _targetRotation = new Vector3(0f, 360f, 0f);

        StartCoroutine(scaleOverTime());
    }

    private IEnumerator scaleOverTime()
    {
        float counter = 0;

        while (counter < _duration)
        {
            counter += Time.deltaTime;
            transform.localScale = Vector3.Lerp(_startScale, _targetScale, counter / _duration);
            transform.eulerAngles = Vector3.Lerp(_startRotation, _targetRotation, counter / _duration);
            
            yield return null;
        }

        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Mob"))
		{
			other.GetComponent<Mob>().TakeDamage(Damage);
		}
	}
}
