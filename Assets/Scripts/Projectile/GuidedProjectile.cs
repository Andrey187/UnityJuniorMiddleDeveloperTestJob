using UnityEngine;
using System;

public class GuidedProjectile : Projectile
{
	protected internal override event Action<ProjectileType, Projectile> ProjectileDeactivated;
   
    protected override void OnTriggerEnter(Collider other) 
	{
		base.OnTriggerEnter(other);
		ProjectileDeactivated?.Invoke(m_projectileType, this);
	}
}
