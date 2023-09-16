using UnityEngine;

public class CannonTowerAttack : TowerAttack, ICannonTowerAttack
{
    [SerializeField] private float m_minRange;
    [SerializeField] private float m_launchAngle = 45f; //Launch angle in degrees
    [SerializeField] private bool m_isCanopyShootingMode = false; // For switching firing mode

    public float LaunchAngle => m_launchAngle;
    public Monster Target => m_target;
    public bool AlternativeAttack { get => m_isCanopyShootingMode; set => m_isCanopyShootingMode = value; }

    private void Update()
    {
        if (m_isCanopyShootingMode)
        {
            // Handling canopy shooting
            SwitchToCanopyShooting();
            m_target = FindClosestTarget();
        }
        else
        {
            //Processing normal shooting
            SwitchToNormalShooting();
            m_target = base.FindClosestTarget();
        }

        SetParameters();

        if (Time.time - m_lastTime >= m_interval)
        {
            HandleShooting();
            m_lastTime = Time.time;
        }
    }
   
    protected override Monster FindClosestTarget()
    {
        Monster closestTarget = null;

        foreach (Monster enemy in m_activeEnemies)
        {
            float distance = Vector3.Distance(ShootPoint.position, enemy.transform.position);

            if (distance >= m_minRange && distance <= m_range)
            {
                if (closestTarget == null || distance <= m_range)
                {
                    closestTarget = enemy;
                }
            }
        }

        return closestTarget;
    }

    protected override void SetParameters()
    {
        m_currentState.SetParameters(m_shootPoint, m_launchAngle, m_target,
       m_projectilePool, m_projectileType);
    }
}

