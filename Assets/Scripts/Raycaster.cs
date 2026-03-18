using System;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    public event Action<RaycastHit> BeamHit;

    [SerializeField] private Camera _camera;
    [SerializeField] private InputReader _inputReader;
    private Ray _ray;

    private void OnEnable() => _inputReader.ClickPerformed += ThrowRay;

    private void OnDisable() => _inputReader.ClickPerformed -= ThrowRay;

    private void ThrowRay()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(_ray, out hit, Mathf.Infinity))
        {
            BeamHit?.Invoke(hit);
        }
    }
}
