using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Bullet _prefab;
    [SerializeField] private Transform _target;
    [SerializeField] private float _delay;

    private Coroutine _coroutine;

    private void Start()
    {
        _coroutine = StartCoroutine(Shoot());
    }

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
    }

    private IEnumerator Shoot()
    {
        bool isWork = enabled;
        var wait = new WaitForSeconds(_delay);

        while (isWork)
        {
            Vector3 direction = (_target.position - transform.position).normalized;
            Bullet bullet = Instantiate(_prefab, transform.position + direction, Quaternion.identity);

            if (bullet.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.transform.up = direction;
                rigidbody.linearVelocity = direction * _speed;
            }            

            yield return wait;
        }
    }
}