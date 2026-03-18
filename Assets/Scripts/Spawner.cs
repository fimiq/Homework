using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public event Action<List<Rigidbody>> SpawnedCubes;
    [SerializeField] private Cube _cubePrafab;
    [SerializeField] private CubeClickHandler _clickHandler;
    private int _currentChance = 100;
    private int _maxCubeCount = 6;
    private int _minCubeCount = 2;

    private void Start()
    {
        CreateCube(Vector3.up, Vector3.one);
    }

    private void OnEnable() => _clickHandler.CubeSplit += FillCubes;

    private void OnDisable() => _clickHandler.CubeSplit -= FillCubes;

    public void FillCubes(Cube cube)
    {
        List<Rigidbody> createdCubes= new List<Rigidbody>();

        _currentChance = cube.SplitChance/2;

        for(int i = 0; i < GetCubeCount(); ++i)
        {
            createdCubes.Add(CreateCube(cube.transform.position, cube.transform.localScale/2).GetComponent<Rigidbody>());
        }

        Destroy(cube.gameObject);

        SpawnedCubes?.Invoke(createdCubes);
    }

    private int GetCubeCount()
    {
        return UnityEngine.Random.Range(_minCubeCount, _maxCubeCount + 1);
    }

    private Cube CreateCube(Vector3 position, Vector3 scale)
    {
        Cube cube = Instantiate(_cubePrafab, position, Quaternion.identity);
        cube.Init(_currentChance);
        ChangeColor(cube.GetComponent<Renderer>().material);
        cube.transform.localScale = scale;

        return cube;   
    }

    private void ChangeColor(Material material)
    {
        material.color = UnityEngine.Random.ColorHSV();
    }
}
