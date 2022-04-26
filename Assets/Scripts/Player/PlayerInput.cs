using MyProject.Events;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	public Camera Camera;
	public Player Player;

	private void Awake()
	{
		EventBus.Sub(PlayerDeadHandler, EventBus.PLAYER_DEATH);
	}

	private void OnDestroy()
	{
		EventBus.Unsub(PlayerDeadHandler, EventBus.PLAYER_DEATH);
	}

	private void PlayerDeadHandler()
	{
		enabled = false;
	}

	void Update()
	{
		var moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

		// Bonus!
		// Show this point as you want in HUD
		var ray = Camera.ScreenPointToRay(Input.mousePosition);

		var plane = new Plane(Vector3.up, Vector3.up * Player.transform.position.y);
		plane.Raycast(ray, out var enter);
		var aimPos = ray.GetPoint(enter);
		var aimInput = aimPos - Player.transform.position;

		var fire = Input.GetKey(KeyCode.Mouse0);
		EventBus<PlayerInputMessage>.Pub(new PlayerInputMessage(
			move: moveInput.normalized,
			aim: new Vector2(aimInput.x, aimInput.z).normalized,
			fire: fire
		));
	}
}