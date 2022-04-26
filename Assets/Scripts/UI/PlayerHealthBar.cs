using TMPro;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
	[SerializeField] private TMP_Text _text;
	[SerializeField] private SpriteRenderer _barImg;
	[SerializeField] private GameObject _bar;
	[SerializeField] private TMP_Text _damageText;

	private Player _player;

	private void Awake()
	{
		_player = GetComponent<Player>();
		_player.HealthChanged += OnHealthChanged;
	}

	private void OnDisable()
	{
		_player.HealthChanged -= OnHealthChanged;
	}

	public void OnDeath()
	{
		_bar.SetActive(false);
	}

	private void LateUpdate()
	{
		_bar.transform.rotation = Camera.main.transform.rotation;
	}

	private void OnHealthChanged(float health)
	{
		float frac = health / _player.MaxHealth;
		_text.text = $"{health:####}/{_player.MaxHealth:####}";
		_barImg.size = new Vector2(frac, _barImg.size.y);
		var pos = _barImg.transform.localPosition;
		pos.x = -(1 - frac) / 2;
		_barImg.transform.localPosition = pos;
		
		if (health <= 0)
		{
			OnDeath();
		}
	}

	private void OnUpgrade()
	{
		_damageText.text = $"{_player.DamageMultiplier}";
	}
}