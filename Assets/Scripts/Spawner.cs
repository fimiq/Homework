using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrafab;
    private int _currentChance = 100;
    private int _maxCubeCount = 6;
    private int _minCubeCount = 2;

    private void Start()
    {
        CreateCube(Vector3.up);
    }

    public List<Rigidbody> FillCubes(Vector3 position, int chance)
    {
        List<Rigidbody> createdCubes= new List<Rigidbody>();

        _currentChance = chance/2;

        for(int i = 0; i < GetCubeCount(); ++i)
        {
            createdCubes.Add(CreateCube(position).GetComponent<Rigidbody>());
        }        

        return createdCubes;
    }

    private int GetCubeCount()
    {
        return UnityEngine.Random.Range(_minCubeCount, _maxCubeCount + 1);
    }

    private Cube CreateCube(Vector3 position)
    {
        Cube cube = Instantiate(_cubePrafab, position, Quaternion.identity);
        cube.Init(this, _currentChance);

        return cube;   
    }
    
}
