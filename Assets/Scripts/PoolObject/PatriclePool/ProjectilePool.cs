using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField] private List<Projectile> m_Projectiles;
    [SerializeField] private int m_countProjectiles;
    [SerializeField] private bool m_autoExpand;
    private Dictionary<ProjectileType, List<Projectile>> m_projectilesDictionary;
    private PoolObject<Projectile> m_ProjectilePool;

    private void Awake()
    {
        m_projectilesDictionary = new Dictionary<ProjectileType, List<Projectile>>();
        InitPool();
    }

    private void InitPool()
    {
        for (int i = 0; i < m_Projectiles.Count; i++)
        {
            List<Projectile> newCachedProjectile = new List<Projectile>();

            ProjectileType projectileType = m_Projectiles[i].m_projectileType;

            // Add the bots to the List
            for (int j = 0; j < m_countProjectiles; j++)
            {
                Projectile projectile = m_Projectiles[i].GetComponent<Projectile>();
                newCachedProjectile.Add(projectile);
            }

            m_projectilesDictionary.Add(projectileType, newCachedProjectile);
        }
        List<Projectile> projectilesToPool = m_projectilesDictionary.SelectMany(pair => pair.Value).ToList();
        PoolObject<Projectile>.CreateInstance(projectilesToPool, gameObject.transform, "_Projectile");
        m_ProjectilePool = PoolObject<Projectile>.Instance;
    }

    // Method for taking a projectile from a pool
    public Projectile GetProjectile(ProjectileType type, Transform startPosition)
    {
        if (m_projectilesDictionary.ContainsKey(type))
        {
            Projectile projectile = m_ProjectilePool.GetObjects(startPosition.position, m_projectilesDictionary[type][0], m_autoExpand);
            projectile.ProjectileDeactivated += ReturnProjectile;
            return projectile;
        }

        return null;
    }

    // Method for returning the projectile to the pool
    private void ReturnProjectile(ProjectileType type, Projectile projectile)
    {
        if (m_projectilesDictionary.ContainsKey(type))
        {
            m_ProjectilePool.ReturnObject(projectile);
            projectile.ProjectileDeactivated -= ReturnProjectile;
        }
    }
}
