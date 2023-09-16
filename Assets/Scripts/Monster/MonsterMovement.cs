using UnityEngine;

public class MonsterMovement : MonoBehaviour, IMonsterMoveable
{
	[SerializeField] private Rigidbody m_rigidbody;
	[SerializeField, Range(m_minValue, m_maxValue)] private int m_speed;
	private const float m_minValue = 0f;
	private const float m_maxValue = 20f;
	private const float m_reachDistance = 3f;
	private IMonster monster;

	public Vector3 MoveTarget { get; set; }

	public int Speed { get => m_speed; set => m_speed = value; }

	public Vector3 RigidbodyVelocity => m_rigidbody.velocity;

    private void Start()
    {
		monster = GetComponent<IMonster>();
	}

    private void Update()
    {
		if (Vector3.Distance(transform.position, MoveTarget) <= m_reachDistance)
		{
            monster.Die();
        }
	}

	private void FixedUpdate()
	{
		Move();
	}

	private void Move()
	{
		if (MoveTarget == null)
			return;

		
		Vector3 direction = (MoveTarget - transform.position).normalized;

        m_rigidbody.velocity = direction * Mathf.Clamp(m_speed, m_minValue, m_maxValue);
    }
}
