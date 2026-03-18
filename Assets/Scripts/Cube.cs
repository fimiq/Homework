using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    public int SplitChance { get; private set; }

    public void Init(int chance)
    {
        SplitChance = chance;
    }
}
