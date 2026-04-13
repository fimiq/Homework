using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AlarmTrigger _alarmTrigger;

    private Coroutine _volumeCoroutine;
    private float _maxVolume = 1f;
    private float _minVolume = 0f;
    private float _changeSpeed = 0.5f;

    private void OnEnable()
    {
        _alarmTrigger.TriggerEnter += AlarmEnable;
        _alarmTrigger.TriggerExit += AlarmDisable;
    }

    private void OnDisable()
    {
        _alarmTrigger.TriggerEnter -= AlarmEnable;
        _alarmTrigger.TriggerExit -= AlarmDisable;
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = _minVolume;
    }

    private void AlarmEnable(Collider collider)
    {
        if (CheckEntryObject(collider))
        {
            if (_volumeCoroutine != null)
            {
                StopCoroutine(_volumeCoroutine);
            }

            _volumeCoroutine = StartCoroutine(ChangeVolume(_maxVolume));
        }
    }

    private void AlarmDisable(Collider collider)
    {
        if (CheckEntryObject(collider))
        {
            if (_volumeCoroutine != null)
            {
                StopCoroutine(_volumeCoroutine);
            }

            _volumeCoroutine = StartCoroutine(ChangeVolume(_minVolume));
        }
    }

    private bool CheckEntryObject(Collider collider)
    {
        return collider.gameObject.TryGetComponent(out RogueMover rogue);
    }

    private IEnumerator ChangeVolume(float targetVolume)
    {
        while (targetVolume - _audioSource.volume != 0)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _changeSpeed * Time.deltaTime);
            yield return null;
        }

        _volumeCoroutine = null;
    }
}