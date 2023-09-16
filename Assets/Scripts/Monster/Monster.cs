using UnityEngine;

public class Monster : MonoBehaviour, IMonster
{

	[SerializeField] private int m_maxHP = 30;
	[SerializeField] private int m_hp;

    private void Start()
    {
		MonsterEventManager.Instance.SetDamage += SetDamage;

	}

    private void OnEnable()
    {
		m_hp = m_maxHP;
	}

    public void SetDamage(GameObject monsterGameObject, int value)
    {
		if(monsterGameObject == gameObject)
        {
			m_hp -= value;
			if (m_hp <= 0)
			{
				Die();
			}
		}
	}

	public void Die()
    {
		MonsterEventManager.Instance.ReturnToPoolObject(this);
	}
}
