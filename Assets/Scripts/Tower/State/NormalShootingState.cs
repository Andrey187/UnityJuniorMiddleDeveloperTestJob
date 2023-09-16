using UnityEngine;

public class NormalShootingState : ITowerState
{
    private Transform ShootPoint;
    private float LaunchAngle;
    private Monster Target;
    private ProjectilePool ProjectilePool;
    private ProjectileType ProjectileType;
    private const int m_speedMultiplier = 3;

    public void SetParameters(
         Transform shootPoint, float launchAngle, Monster target,
         ProjectilePool projectilePool, ProjectileType projectileType)
    {
        ShootPoint = shootPoint;
        LaunchAngle = launchAngle;
        Target = target;
        ProjectilePool = projectilePool;
        ProjectileType = projectileType;
    }


    public void HandleShooting()
    {
        if (Target != null)
        {
            FireProjectile(Target);
        }
    }

    private Vector3 CalculateInterceptPosition(Monster target, IProjectileMoveable projectileMoveable)
    {
        target.TryGetComponent(out IMoveable moveable);

        Vector3 targetDirection = target.transform.position - ShootPoint.transform.position;

        projectileMoveable.Speed = m_speedMultiplier * moveable.Speed;

        float timeToReachTarget = targetDirection.magnitude / projectileMoveable.Speed;

        Vector3 targetPosition = target.transform.position + moveable.RigidbodyVelocity * timeToReachTarget;
        return targetPosition;
    }

    private void FireProjectile(Monster m_target)
    {
        if (m_target != null)
        {
            Projectile projectile = ProjectilePool.GetProjectile(ProjectileType, ShootPoint);
            projectile.TryGetComponent(out IProjectileMoveable projectileMoveable);

            Vector3 targetPosition = CalculateInterceptPosition(m_target, projectileMoveable);

            projectileMoveable.StartShootPoint = ShootPoint.transform;

            projectileMoveable.MoveNormalProjectile(ShootPoint.transform.position, targetPosition);
        }
    }
}
