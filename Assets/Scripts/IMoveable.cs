using UnityEngine;

public interface IMoveable
{
    public int Speed { get; set; }

    public Vector3 RigidbodyVelocity { get; }
   
}
