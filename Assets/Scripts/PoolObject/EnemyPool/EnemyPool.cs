using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private Monster m_monsters;
    [SerializeField] private GameObject m_startPosition;
    [SerializeField] private GameObject m_moveTarget;
    [SerializeField] private bool m_autoExpand;
    [SerializeField] private int m_countMonsters;
    [SerializeField] private float m_interval = 3;
    private float m_lastSpawn = -1;
    private List<Monster> newCachedMonster = new List<Monster>();
    private PoolObject<Monster> m_MonsterPool;


    private void Awake()
    {
        InitPool();
    }

    private void Update()
    {
        if (Time.time > m_lastSpawn + m_interval)
        {
            GetMonsterFromPool(m_startPosition.transform);
            
            m_lastSpawn = Time.time;
        }
    }

    private void InitPool()
    {
        // Add the bots to the List
        for (int j = 0; j < m_countMonsters; j++)
        {
            newCachedMonster.Add(m_monsters);
        }
        
        PoolObject<Monster>.CreateInstance(newCachedMonster, gameObject.transform, "_Monster");
        m_MonsterPool = PoolObject<Monster>.Instance;
    }

    private void GetMonsterFromPool(Transform startPosition)
    {
        Monster monster = m_MonsterPool.GetObjects(startPosition.position, newCachedMonster[0], m_autoExpand);

        MonsterEventManager.Instance.ObjectReturnToPool += ReturnMonsterToPool;
        MonsterEventManager.Instance.ActivatedObject(monster);

        monster.TryGetComponent(out IMonsterMoveable moveable);
        moveable.MoveTarget = m_moveTarget.transform.position;
    }


    // Method for returning the projectile to the pool
    private void ReturnMonsterToPool(Monster monster)
    {
        m_MonsterPool.ReturnObject(monster);

        MonsterEventManager.Instance.ObjectReturnToPool -= ReturnMonsterToPool;
        MonsterEventManager.Instance.DeactivatedObject(monster);
    }
    
}
