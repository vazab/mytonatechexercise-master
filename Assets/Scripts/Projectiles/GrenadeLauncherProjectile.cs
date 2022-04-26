using UnityEngine;

public class GrenadeLauncherProjectile : Projectile
{
    [SerializeField] private Explosion _explosion;

    private void Start()
    {
        _explosion.Damage = Damage;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        Instantiate(_explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
