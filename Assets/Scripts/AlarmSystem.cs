using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private bool _isWork = false;
    private int _maxVolume = 1;
    private int _minVolume = 0;
    private int _targetVolume = 0;
    private float _changeSpeed = 0.5f;
   
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _targetVolume, _changeSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isWork == false)
        {
            _isWork = true;
            _targetVolume = _maxVolume;
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        if (_isWork == true)
        {
            _isWork = false;
            _targetVolume = _minVolume;
        }
    }
}