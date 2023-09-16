using UnityEngine;

public interface ITowerState
{
    public void SetParameters(Transform shootPoint, float launchAngle, Monster target,
        ProjectilePool projectilePool, ProjectileType projectileType);

    public void HandleShooting();
}
