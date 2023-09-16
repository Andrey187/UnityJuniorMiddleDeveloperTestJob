using UnityEngine;

public class CrystalTowerAttack : TowerAttack
{
    private float m_launchAngle = 0;

    private void Update()
    {
        SwitchToNormalShooting();

        m_target = FindClosestTarget();

        SetParameters();

        if (Time.time - m_lastTime >= m_interval)
        {
            HandleShooting();
            m_lastTime = Time.time;
        }
    }

    protected override void SetParameters()
    {
        m_currentState.SetParameters(m_shootPoint, m_launchAngle, m_target,
       m_projectilePool, m_projectileType);
    }

}
