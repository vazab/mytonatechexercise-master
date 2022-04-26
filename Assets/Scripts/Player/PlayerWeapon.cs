using MyProject.Events;
using UnityEngine;

[RequireComponent(typeof(Player), typeof(PlayerAnimator))]
public abstract class PlayerWeapon : MonoBehaviour
{
    [SerializeField] protected Projectile BulletPrefab;
    [SerializeField] protected Transform FirePoint;
    [SerializeField] protected float Damage = 1f;
    [SerializeField] protected float Reload = 1f;
    [SerializeField] protected ParticleSystem Vfx;
    [SerializeField] private GameObject _model;

    protected const int RIFLE = 0;
    protected const int SHOTGUN = 1;
    protected const int AUTOMATIC_RIFLE = 2;
	protected const int GRENADE_LAUNCHER = 3;
    protected float LastTime;

    public abstract int Type { get; }

    protected virtual void Awake()
    {
        GetComponent<Player>().WeaponChanged += OnChanged;

        LastTime = Time.time - Reload;
    }

    protected virtual void OnDisable()
    {
        EventBus<PlayerInputMessage>.Unsub(Fire);
        GetComponent<Player>().WeaponChanged -= OnChanged;
    }

    protected void OnChanged(int type)
    {
        EventBus<PlayerInputMessage>.Unsub(Fire);

        if (type == Type)
        {
            EventBus<PlayerInputMessage>.Sub(Fire);
        }

        _model.SetActive(type == Type);
    }

    protected virtual float GetDamage()
    {
        var totalDamage = Damage * GetComponent<Player>().DamageMultiplier;
        return totalDamage;
    }

    protected abstract void Fire(PlayerInputMessage message);
}