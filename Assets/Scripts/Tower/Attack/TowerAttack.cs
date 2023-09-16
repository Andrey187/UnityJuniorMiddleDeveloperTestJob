using System.Collections.Generic;
using UnityEngine;

public abstract class TowerAttack : MonoBehaviour, ITowerAttack
{
	[SerializeField] protected ProjectileType m_projectileType;
	[SerializeField] protected float m_interval = 0.5f;
	[SerializeField] protected float m_range = 2f;
	[SerializeField] protected Transform m_shootPoint;
    protected float m_lastTime = -0.5f;
    protected Monster m_target;
	protected List<Monster> m_activeEnemies = new List<Monster>();
	protected ProjectilePool m_projectilePool;
    protected ITowerState m_currentState;

    public Transform ShootPoint => m_shootPoint;

    protected void Awake()
    {
		m_projectilePool = FindObjectOfType<ProjectilePool>();
    }

	private void Start()
	{
        // Subscribe to enemy activation and deactivation events
        MonsterEventManager.Instance.ObjectActivated += HandleEnemyActivated;
		MonsterEventManager.Instance.ObjectDeactivated += HandleEnemyDeactivated;
    }

	private void HandleEnemyActivated(Monster enemy)
	{
        // Adding an active enemy to the list
        m_activeEnemies.Add(enemy);
	}

	private void HandleEnemyDeactivated(Monster enemy)
	{
        //Removing an inactive enemy from the list
        m_activeEnemies.Remove(enemy);
	}

    protected virtual Monster FindClosestTarget()
    {
        Monster closestTarget = null;

        foreach (Monster enemy in m_activeEnemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance <= m_range)
            {
                if (closestTarget == null || distance < Vector3.Distance(transform.position, closestTarget.transform.position))
                {
                    closestTarget = enemy;
                }
            }
        }

        return closestTarget;
    }

    protected void SwitchToNormalShooting()
    {
        m_currentState = new NormalShootingState();
    }

    protected void SwitchToCanopyShooting()
    {
        m_currentState = new CanopyShootingState();
    }

    protected abstract void SetParameters();

    protected void HandleShooting()
    {
        m_currentState.HandleShooting();
    }
}
