using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    private Animator _animator;

    private readonly int IsAttack = Animator.StringToHash("IsAttack");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetAttack() =>
        _animator.SetTrigger(IsAttack);
}
