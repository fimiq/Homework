using UnityEngine;

public class PointMover : MonoBehaviour
{
    [SerializeField] private Transform _parentPlace;
    [SerializeField] private Transform[] _places;
    [SerializeField] private float _speed;

    private int _currnetPlace;
    
    private void Start()
    {
        _places = new Transform[_parentPlace.childCount];

        for (int i = 0; i < _parentPlace.childCount; i++)
            _places[i] = _parentPlace.GetChild(i).GetComponent<Transform>();
    }

    private void Update()
    {
        Transform place = _places[_currnetPlace];

        transform.position = Vector3.MoveTowards(transform.position, place.position, _speed * Time.deltaTime);

        if (transform.position == place.position)
        {
            GetNextPlace();
        }
    }

    private Vector3 GetNextPlace()
    {
        _currnetPlace++;

        if (_currnetPlace == _places.Length)
            _currnetPlace = 0;

        Vector3 nextPlace = _places[_currnetPlace].transform.position;

        return nextPlace;
    }
}