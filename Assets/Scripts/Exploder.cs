using UnityEngine;
using System.Collections.Generic;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explotionForce;
    [SerializeField] private float _explotionRadius;

    private float _offsetMultiplie = 0.5f;
    public void Explode(IEnumerable<Cube> cubes)
    {
        foreach (Cube explodableObjcets in cubes)
        {
            Vector3 randomOffset = Random.insideUnitSphere * _offsetMultiplie;
            Vector3 explosionPosition = transform.position + randomOffset;

            explodableObjcets.Rigidbody.AddExplosionForce(_explotionForce, explosionPosition, _explotionRadius);
        }
    }
}
