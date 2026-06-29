using UnityEngine;

public class EnemyChaser : MonoBehaviour
{
    [SerializeField] private float _viewDistance = 5f;
    [SerializeField] private float _viewAngle = 90f;
    [SerializeField] private LayerMask _obstacleLayer;

    private Transform _target;
    private bool _hasSeenTarget;

    public bool HasTarget => _hasSeenTarget;
    public Vector3 TargetPosition => _target.position;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
            _target = player.transform;
    }

    private void Update()
    {
        if (_target == null)
            return;

        if (CanSeeTarget())
        {
            _hasSeenTarget = true;
        }
        else if (Vector2.Distance(transform.position, _target.position) > _viewDistance)
        {
            _hasSeenTarget = false;
        }
    }

    private bool CanSeeTarget()
    {
        float distance = Vector2.Distance(transform.position, _target.position);

        if (distance > _viewDistance)
            return false;

        Vector2 facingDirection = transform.rotation.eulerAngles.y == 0f
            ? Vector2.right
            : Vector2.left;

        Vector2 directionToTarget = (_target.position - transform.position).normalized;

        float angle = Vector2.Angle(facingDirection, directionToTarget);

        if (angle > _viewAngle / 2f)
            return false;

        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            directionToTarget,
            distance,
            _obstacleLayer);

        return hit.collider == null;
    }
}
