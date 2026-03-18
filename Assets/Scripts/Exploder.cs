using System;
using UnityEngine;
using System.Collections.Generic;

public class Exploder : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private float _explotionForce;
    [SerializeField] private float _explotionRadius;

    private void OnEnable() => _spawner.SpawnedCubes += Explode;

    private void OnDisable() => _spawner.SpawnedCubes -= Explode;

    private void Explode(List<Rigidbody> cubes)
    {
        foreach (Rigidbody explodableObjcets in cubes)
        {
            explodableObjcets.AddExplosionForce(_explotionForce, transform.position, _explotionRadius);
        }
    }
}
