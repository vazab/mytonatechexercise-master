using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpWithWeight", menuName = "Sciptable Objects/PowerUp/PowerUp With Weight", order = 51)]
public class PowerUpWithWeight : ScriptableObject
{
    [SerializeField] private GameObject _prefab;
    [SerializeField, Range(0.01f, 1f)] private float _weight;

    public GameObject Prefab => _prefab;
    public float Weight => _weight;
}