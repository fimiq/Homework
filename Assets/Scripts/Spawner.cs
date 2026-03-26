using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrafab;

    private int _currentChance = 100;
    private int _chanceReduction = 2;
    private int _scaleReduction = 2;
    private int _maxCubeCount = 6;
    private int _minCubeCount = 2;

    private void Start()
    {
        CreateCube(Vector3.up, Vector3.one);
    }

    public List<IExplodable> SplitCubes(Cube cube)
    {
        List<IExplodable> createdCubes= new List<IExplodable>();

        _currentChance = cube.SplitChance/_chanceReduction;

        int cubesCount = GetCubeCount();

        for(int i = 0; i < cubesCount; ++i)
        {
            Cube newCube = CreateCube(cube.transform.position, cube.transform.localScale / _scaleReduction);
            createdCubes.Add(newCube);
        }

        return createdCubes;
    }

    public void DestroyCube(Cube cube)
    {
        Destroy(cube.gameObject);   
    }

    private int GetCubeCount()
    {
        return UnityEngine.Random.Range(_minCubeCount, _maxCubeCount + 1);
    }

    private Cube CreateCube(Vector3 position, Vector3 scale)
    {
        Cube cube = Instantiate(_cubePrafab, position, Quaternion.identity);
        cube.Init(_currentChance);

        if(cube.TryGetComponent<Renderer>(out Renderer renderer))
        {
            ChangeColor(renderer.material);
        }
        
        cube.transform.localScale = scale;

        return cube;   
    }

    private void ChangeColor(Material material)
    {
        material.color = UnityEngine.Random.ColorHSV();
    }
}
