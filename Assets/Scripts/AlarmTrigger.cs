using System;
using UnityEngine;

public class AlarmTrigger : MonoBehaviour
{   
    public event Action TriggerEnter;
    public event Action TriggerExit;

    private void OnTriggerEnter(Collider other)
    {
        if (CheckEntryObject(other))
        {
            TriggerEnter?.Invoke();
        }        
    }

    private void OnTriggerExit(Collider other)
    {
        if (CheckEntryObject(other))
        {
            TriggerExit?.Invoke();
        }        
    }

    private bool CheckEntryObject(Collider collider)
    {
        return collider.gameObject.TryGetComponent(out RogueMover rogue);
    }
}