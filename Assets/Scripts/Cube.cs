using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private const int MaxSplitChance = 100;

    [SerializeField] private float _explotionForce;
    [SerializeField] private float _explotionRadius;

    private Spawner _spawner;
    private Transform _transform;
    private List<Rigidbody>_createdCubes;
    private Material material;

    private int _splitChance;

    public void Init(Spawner spawner, int chance)
    {
        _spawner = spawner;
        _splitChance = chance;
        material = GetComponent<MeshRenderer>().material;
        ChangeColor();
    }

    private void ChangeColor()
    {
        material.color = UnityEngine.Random.ColorHSV();
    }

    private void OnMouseUpAsButton()
    {
        if (TrySplitCube())
        {
            _createdCubes = _spawner.FillCubes(transform.position, _splitChance);
            Explode();
        }

        Destroy(gameObject);
    }

    private bool TrySplitCube()
    {
        int randomNumber = new System.Random().Next(0, MaxSplitChance+1);
        return _splitChance >= randomNumber;
    }

    private void Explode()
    {
        foreach(Rigidbody explodableOnjcets in _createdCubes)
        {
            explodableOnjcets.AddExplosionForce(_explotionForce, transform.position, _explotionRadius);
        }
    }
}
