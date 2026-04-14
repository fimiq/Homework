using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] private AlarmTrigger _alarmTrigger;
    [SerializeField] private AlarmSystem _alarmSystem;

    private void OnEnable()
    {
        _alarmTrigger.TriggerEnter += _alarmSystem.AlarmEnable;
        _alarmTrigger.TriggerExit += _alarmSystem.AlarmDisable;
    }

    private void OnDisable()
    {
        _alarmTrigger.TriggerEnter -= _alarmSystem.AlarmEnable;
        _alarmTrigger.TriggerExit -= _alarmSystem.AlarmDisable;
    }
}
