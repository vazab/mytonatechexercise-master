using MyProject.Events;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
	[SerializeField] private Player player;
    [SerializeField] private PowerUpWithWeight[] _powerUps;

    private float _totalWeight = 0;

    private void Awake()
    {
        _totalWeight = GetTotalWeight();

        EventBus.Sub(Handle, EventBus.MOB_KILLED);
    }

    private void OnDisable()
    {
        EventBus.Unsub(Handle, EventBus.MOB_KILLED);
    }

    private void Handle()
    {
        SpawnWithWeights(PickRandomPosition());
    }

    private Vector3 PickRandomPosition()
    {
        var vector3 = new Vector3();
        vector3.x = Random.Range(-5f, 5f);
        vector3.z = Random.Range(-5f, 5f);

        return vector3;
    }

    private void SpawnWithWeights(Vector3 position)
    {
        float rand = Random.value;

        foreach (PowerUpWithWeight powerUp in _powerUps)
        {
            if (rand <= powerUp.Weight / _totalWeight)
            {
                if (CompareWithCurrentWeapon(powerUp) == false)
                {
                    Instantiate(powerUp.Prefab, position, Quaternion.identity);
                    return;
                }
            }

            rand -= powerUp.Weight / _totalWeight;
        }
    }

    private bool CompareWithCurrentWeapon(PowerUpWithWeight powerUp)
    {
        if (powerUp.Prefab.TryGetComponent(out WeaponPowerUp weapon))
        {
            return (weapon.Type == player.CurrentWeapon);
        }

        return false;
    }

    private float GetTotalWeight()
    {
        _totalWeight = 0;

        foreach (PowerUpWithWeight powerUp in _powerUps)
        {
            _totalWeight += powerUp.Weight;
        }

        return _totalWeight;
    }
}