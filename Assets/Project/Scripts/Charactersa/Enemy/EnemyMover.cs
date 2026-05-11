using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;

    public void MoveTo(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            _speed * Time.deltaTime);
    }
}