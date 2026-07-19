using System.Collections;
using UnityEngine;

public class EnemyChaser : MonoBehaviour
{
    [SerializeField] private float _viewDistance = 5f;
    [SerializeField] private float _scanInterval = 0.1f;
    [SerializeField] private LayerMask _obstacleLayer;

    public bool HasTarget   { get; private set; }
    public Vector3 TargetPosition => _targetTransform != null
        ? _targetTransform.position
        : transform.position;

    private Transform _targetTransform;

    private void OnEnable()  => StartCoroutine(ScanRoutine());
    private void OnDisable() => StopAllCoroutines();

    public void SetTarget(Transform target)
    {
        _targetTransform = target;
    }

    private IEnumerator ScanRoutine()
    {
        var wait = new WaitForSeconds(_scanInterval);

        while (enabled)
        {
            HasTarget = _targetTransform != null && CanSeeTarget();
            yield return wait;
        }
    }

    private bool CanSeeTarget()
    {
        Vector2 toTarget = (Vector2)(_targetTransform.position - transform.position);
        float sqrDist = toTarget.sqrMagnitude;

        if (sqrDist > _viewDistance * _viewDistance)
            return false;

        float dist = Mathf.Sqrt(sqrDist);
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            toTarget / dist,
            dist,
            _obstacleLayer);

        return hit.collider == null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _viewDistance);
    }
}
