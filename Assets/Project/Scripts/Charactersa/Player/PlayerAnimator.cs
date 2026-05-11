using UnityEngine;

[RequireComponent (typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;

    private readonly int IsRun = Animator.StringToHash("IsRun");

    public void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetMove(float speed)
    {
        _animator.SetBool(IsRun, speed > 0.1f);
    }
}
