using System;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private InputReader _inputReader;
    private Ray _ray;

    public event Action<RaycastHit> BeamHit;

    private void OnEnable() => 
        _inputReader.ClickPerformed += ThrowRay;

    private void OnDisable() => 
        _inputReader.ClickPerformed -= ThrowRay;

    private void ThrowRay()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(_ray, out RaycastHit hit, Mathf.Infinity))
        {
            BeamHit?.Invoke(hit);
        }
    }
}
