using MyProject.Events;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private void Awake()
    {
        EventBus<PlayerInputMessage>.Sub(Move);
    }

	private void OnDisable()
	{
		EventBus<PlayerInputMessage>.Unsub(Move);
	}

    private void Move(PlayerInputMessage message)
    {
        var speed = GetComponent<Player>().MoveSpeed;
        var delta = new Vector3(speed * message.MovementDirection.x, 0, speed * message.MovementDirection.y) * Time.deltaTime;
        transform.position += delta;
        transform.forward = new Vector3(message.AimDirection.x, 0, message.AimDirection.y);
    }
}