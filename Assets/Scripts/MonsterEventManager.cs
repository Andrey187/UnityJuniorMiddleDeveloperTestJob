using System;
using UnityEngine;

public class MonsterEventManager : MonoBehaviour
{
    private Action<Monster> m_objectActivated;
    private Action<Monster> m_objectDeactivated;
    private Action<Monster> m_objectReturnToPool;
    private Action<GameObject, int> m_setDamage;

    private static MonsterEventManager _instance;
    [RuntimeInitializeOnLoadMethod]
    static void Initialize()
    {
        if (_instance == null)
        {
            GameObject obj = new GameObject("MonsterEventManager");
            _instance = obj.AddComponent<MonsterEventManager>();
            DontDestroyOnLoad(obj);
        }
    }

    public static MonsterEventManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MonsterEventManager>();
            }

            return _instance;
        }
    }

    public event Action<Monster> ObjectActivated
    {
        add { m_objectActivated += value; }
        remove { m_objectActivated -= value; }
    }

    public event Action<Monster> ObjectDeactivated
    {
        add { m_objectDeactivated += value; }
        remove { m_objectDeactivated -= value; }
    }

    public event Action<Monster> ObjectReturnToPool
    {
        add { m_objectReturnToPool += value; }
        remove { m_objectReturnToPool -= value; }
    }

    public event Action<GameObject, int> SetDamage
    {
        add { m_setDamage += value; }
        remove { m_setDamage -= value; }
    }

    public void ActivatedObject(Monster monster)
    {
        m_objectActivated?.Invoke(monster);
    }

    public void DeactivatedObject(Monster monster)
    {
        m_objectDeactivated?.Invoke(monster);
    }

    public void ReturnToPoolObject(Monster monster)
    {
        m_objectReturnToPool?.Invoke(monster);
    }

    public void Damage(GameObject gameObject, int value)
    {
        m_setDamage?.Invoke(gameObject, value);
    }
}
