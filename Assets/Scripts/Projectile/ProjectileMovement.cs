using UnityEngine;

public class ProjectileMovement : MonoBehaviour, IProjectileMoveable
{
    [SerializeField] private int m_speed = 10;
    [SerializeField] private Rigidbody m_rb;

    public int Speed { get => m_speed; set => m_speed = value; }

    public Vector3 RigidbodyVelocity => m_rb.velocity;
    public Transform StartShootPoint { get; set; }
    public Rigidbody Rigidbody { get => m_rb; set => m_rb = value; }
   
    private void OnDisable()
    {
        m_rb.velocity = Vector3.zero;
    }

    public void MoveCannonProjectile(Transform startPoint, float calculatedSpeed)
    {
        
        m_rb.velocity = startPoint.forward * calculatedSpeed;
    }

    public void MoveNormalProjectile(Vector3 startPoint, Vector3 target)
    {
        Vector3 direction = (target - startPoint).normalized;

        m_rb.velocity = direction * Speed;
    }
}
       

