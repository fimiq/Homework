using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private Coroutine _volumeCoroutine;
    private float _maxVolume = 1f;
    private float _minVolume = 0f;
    private float _changeSpeed = 0.5f;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = _minVolume;
    }

    public void AlarmEnable() =>
        SetTargetVolume(_maxVolume);
    
    public void AlarmDisable() =>
        SetTargetVolume(_minVolume);       

    private void SetTargetVolume(float volume)
    {
        if (_volumeCoroutine != null)
        {
            StopCoroutine(_volumeCoroutine);
        }

        _volumeCoroutine = StartCoroutine(ChangeVolume(volume));
    }

    private IEnumerator ChangeVolume(float targetVolume)
    {
        if (targetVolume > _minVolume && !_audioSource.isPlaying)
        {
            _audioSource.Play();
        }

        while (targetVolume != _audioSource.volume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _changeSpeed * Time.deltaTime);
            yield return null;
        }

        if (targetVolume == _minVolume && _audioSource.isPlaying)
        {
            _audioSource.Stop();
        }

        _volumeCoroutine = null;
    }
}