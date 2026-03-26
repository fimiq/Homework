using UnityEngine;
using System.Collections.Generic;

public class Exploder : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explotionParticle;
    [SerializeField] private float _explotionForce = 200;
    [SerializeField] private float _explotionRadius = 2;

    private float _offsetMultiplie = 0.5f;

    public void Explode(IEnumerable<IExplodable> cubes)
    {
        foreach (IExplodable explodableObjcet in cubes)
        {
            Vector3 randomOffset = Random.insideUnitSphere * _offsetMultiplie;
            Vector3 explosionPosition = explodableObjcet.Position + randomOffset;

            explodableObjcet.Rigidbody.AddExplosionForce(_explotionForce, explosionPosition, _explotionRadius);
        }
    }

    public void Explode(IExplodable explodable)
    {
        float force = GetExplotionForce(explodable.Size);
        float radius = GetExplotionRadius(explodable.Size);

        foreach (Rigidbody explodableObject in GetExplodableObjects(explodable))
        {
            explodableObject.AddExplosionForce(force, explodable.Position, radius);
        }

        Instantiate(_explotionParticle, explodable.Position, Quaternion.identity);
    }

    private float GetExplotionForce(float size) =>
        _explotionForce / size;

    private float GetExplotionRadius(float size) =>
        _explotionRadius / size;

    private List<Rigidbody> GetExplodableObjects(IExplodable explodable)
    {
        Collider[] hits = Physics.OverlapSphere(explodable.Position, _explotionRadius);

        List<Rigidbody> explodableObjects = new();

        foreach (Collider collider in hits)
        {
            if(collider.attachedRigidbody != null)
            {
                explodableObjects.Add(collider.attachedRigidbody);
            }
        }

        return explodableObjects;
    }
}
