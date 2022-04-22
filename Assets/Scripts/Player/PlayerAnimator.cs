using MyProject.Events;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        EventBus<PlayerInputMessage>.Sub(AnimateRun);
        EventBus.Sub(AnimateDeath, EventBus.PLAYER_DEATH);
    }

    private void OnDisable()
    {
        EventBus<PlayerInputMessage>.Unsub(AnimateRun);
		EventBus.Unsub(AnimateDeath, EventBus.PLAYER_DEATH);
    }

    private void AnimateRun(PlayerInputMessage message)
    {
        _animator.SetBool("IsRun", message.MovementDirection.sqrMagnitude > 0);
    }

    private void AnimateDeath()
    {
        _animator.SetTrigger("Death");
    }

    public void TriggerShoot()
    {
        _animator.SetTrigger("Shoot");
    }
}