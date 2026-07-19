using UnityEngine;

[RequireComponent (typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;

    private readonly int IsRun = Animator.StringToHash("IsRun");
    private readonly int IsAttack = Animator.StringToHash("IsAttack");
   
    public void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetMove(float speed) => 
        _animator.SetBool(IsRun, speed > 0.1f);

    public void SetAttack() => 
        _animator.SetTrigger(IsAttack);
}
