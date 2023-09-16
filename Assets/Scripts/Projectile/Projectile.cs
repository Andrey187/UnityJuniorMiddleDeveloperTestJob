using System;
using UnityEngine;

public enum ProjectileType
{
	Crystal,
	Cannon
}

public abstract class Projectile : MonoBehaviour
{
	[SerializeField] protected internal ProjectileType m_projectileType;
	[SerializeField] protected int m_damage = 10;
	[SerializeField] protected LayerMask m_enemyLayer;
	
    protected internal virtual event Action<ProjectileType, Projectile> ProjectileDeactivated;

	protected virtual void OnTriggerEnter(Collider other)
	{
		if ((m_enemyLayer.value & (1 << other.transform.gameObject.layer)) > 0)
		{
			MonsterEventManager.Instance.Damage(other.gameObject, m_damage);
		}
	}
}
