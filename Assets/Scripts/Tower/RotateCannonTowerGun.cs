using UnityEngine;

public class RotateCannonTowerGun : MonoBehaviour
{
    [SerializeField] private GameObject m_gun;
    [SerializeField] private GameObject m_core;
    [SerializeField] private float m_turningSpeed;
    [SerializeField] private float m_angleTurningAccuracy;
    private ICannonTowerAttack m_towerAttack;

    private void Start()
    {
        m_towerAttack = GetComponent<ICannonTowerAttack>();
    }

    private void Update()
    {
        if (m_towerAttack.Target != null)
        {
            if(m_towerAttack.AlternativeAttack == false)
            {
                RotateForNormalAttackState();
            }
            else
            {
                RotateForCanopyAttackState();
            }
        }
    }

    private void RotateForNormalAttackState()
    {
        Vector3 aimAt = new Vector3(m_towerAttack.Target.transform.position.x, m_core.transform.position.y, m_towerAttack.Target.transform.position.z);
        float distToTarget = Vector3.Distance(aimAt, m_gun.transform.position);

        Vector3 relativeTargetPosition = m_gun.transform.position + (m_gun.transform.forward * distToTarget);

        relativeTargetPosition = new Vector3(relativeTargetPosition.x, m_towerAttack.Target.transform.position.y, relativeTargetPosition.z);

        m_gun.transform.rotation = Quaternion.Slerp(m_gun.transform.rotation, Quaternion.LookRotation(relativeTargetPosition - m_gun.transform.position), Time.deltaTime * m_turningSpeed);
        m_core.transform.rotation = Quaternion.Slerp(m_core.transform.rotation, Quaternion.LookRotation(aimAt - m_core.transform.position), Time.deltaTime * m_angleTurningAccuracy);
    }

    private void RotateForCanopyAttackState()
    {
        Quaternion startRotation = m_gun.transform.localRotation;
        Quaternion targetRotation = Quaternion.Euler(-m_towerAttack.LaunchAngle, 0f, 0f);
        m_gun.transform.localRotation = Quaternion.Slerp(startRotation, targetRotation, m_turningSpeed * Time.deltaTime);


        Vector3 targetDirection = m_towerAttack.Target.transform.position - m_towerAttack.ShootPoint.transform.position;
        Vector3 targetDirectionXZ = new Vector3(targetDirection.x, 0f, targetDirection.z);

        m_core.transform.rotation = Quaternion.Slerp(m_core.transform.rotation, Quaternion.LookRotation(targetDirectionXZ, Vector3.up), Time.deltaTime * m_turningSpeed);
    }
}
