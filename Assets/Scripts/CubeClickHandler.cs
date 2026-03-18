using System;
using UnityEngine;

public class CubeClickHandler : MonoBehaviour
{
    private const int MaxSplitChance = 100;

    public event Action<Cube> CubeSplit;

    [SerializeField] private Raycaster _raycaster;

    private void OnEnable() => _raycaster.BeamHit += TrySplitCube;

    private void OnDisable() => _raycaster.BeamHit -= TrySplitCube;

    private void TrySplitCube(RaycastHit hit)
    {
        Cube cube = hit.transform.GetComponent<Cube>();

        if (cube != null)
        {
            int randomNumber = new System.Random().Next(0, MaxSplitChance + 1);

            if (cube.SplitChance >= randomNumber)
            {
                CubeSplit?.Invoke(cube);
            }
            else
            {
                Destroy(hit.collider.gameObject);
            }
        }
        else
        {
            return;
        }
    }
}
