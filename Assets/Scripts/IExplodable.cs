using UnityEngine;

public interface IExplodable 
{
    Rigidbody Rigidbody { get; }
    float Size { get; }
    Vector3 Position { get; }
}
