using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CubeSplitter : MonoBehaviour
{
    private const int MaxSplitChance = 100;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Exploder _exploder;
    [SerializeField] private Raycaster _raycaster;

    private void OnEnable() =>
        _raycaster.BeamHit += TrySplitCube;

    private void OnDisable() =>
        _raycaster.BeamHit -= TrySplitCube;

    private void TrySplitCube(RaycastHit hit)
    {
        int randomNumber = UnityEngine.Random.Range(0, MaxSplitChance + 1);

        if (hit.transform.TryGetComponent(out Cube cube))
        {
            if (cube.SplitChance >= randomNumber)
            {
                List<IExplodable> cubes = _spawner.SplitCubes(cube);
                _exploder.Explode(cubes);
            }
            else
            {
                _exploder.Explode(cube);
            }

             _spawner.DestroyCube(cube);
        }
    }
}
