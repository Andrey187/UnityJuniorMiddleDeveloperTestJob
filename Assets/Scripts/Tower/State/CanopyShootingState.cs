using UnityEngine;

public class CanopyShootingState : ITowerState
{
    private Transform m_shootPoint;
    private float m_launchAngle;
    private Monster m_target;
    private ProjectilePool m_projectilePool;
    private ProjectileType m_projectileType;

    private float g = Physics.gravity.y;
    private float m_calculatedSpeed;

    public void SetParameters(
        Transform shootPoint, float launchAngle, Monster target,
        ProjectilePool projectilePool, ProjectileType projectileType)
    {
        m_shootPoint = shootPoint;
        m_launchAngle = launchAngle;
        m_target = target;
        m_projectilePool = projectilePool;
        m_projectileType = projectileType;
    }


    public void HandleShooting()
    {
        if (m_target != null)
        {
            m_calculatedSpeed = Calculate(m_target);
            CanopyFireProjectile(m_calculatedSpeed);
        }
    }

    private float Calculate(Monster target)
    {
        Vector3 targetDirection = target.transform.position - m_shootPoint.transform.position;
        Vector3 targetDirectionXZ = new Vector3(targetDirection.x, 0f, targetDirection.z);

        float x = targetDirectionXZ.magnitude;
        float y = targetDirection.y;

        float AngleInRadians = m_launchAngle * Mathf.PI / 180;

        float calculatedSpeedSquared = (g * x * x) / (2 * (y - Mathf.Tan(AngleInRadians) * x) * Mathf.Pow(Mathf.Cos(AngleInRadians), 2));
        float calculatedSpeed = Mathf.Sqrt(Mathf.Abs(calculatedSpeedSquared));

        return calculatedSpeed;
    }

    private void CanopyFireProjectile(float calculatedSpeed)
    {
        if (m_target != null)
        {
            Projectile projectile = m_projectilePool.GetProjectile(m_projectileType, m_shootPoint);
            projectile.TryGetComponent(out IProjectileMoveable projectileMoveable);

            projectileMoveable.MoveCannonProjectile(m_shootPoint, calculatedSpeed);
        }
    }
}
