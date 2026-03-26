using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour , IExplodable
{
    public int SplitChance { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public float Size => transform.localScale.x;
    public Vector3 Position => transform.position;

    public void Init(int chance)
    {
        Rigidbody = GetComponent<Rigidbody>();
        SplitChance = chance;
    }
}
