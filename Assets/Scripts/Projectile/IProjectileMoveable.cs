using UnityEngine;

public interface IProjectileMoveable : IMoveable
{
    public Transform StartShootPoint { get; set; }

    public Rigidbody Rigidbody { get; set; }

    public void MoveNormalProjectile(Vector3 startPoint, Vector3 targetPoint);

    public void MoveCannonProjectile(Transform startPoint, float calculatedSpeed);

}
