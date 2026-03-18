using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    public int SplitChance { get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    public void Init(int chance)
    {
        Rigidbody = GetComponent<Rigidbody>();
        SplitChance = chance;
    }
}
