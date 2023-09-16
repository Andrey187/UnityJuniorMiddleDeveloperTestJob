using UnityEngine;
using UnityEngine.UI;

public class ButtonChangeFireState : MonoBehaviour
{
    [SerializeField] private TowerAttack m_towerAttack;
    [SerializeField] private Button m_button;
    private ICannonTowerAttack m_cannonTowerAttack;

    private void Start()
    {
        m_cannonTowerAttack = m_towerAttack.GetComponent<ICannonTowerAttack>();

        m_button.onClick.AddListener(() => ChangeState());
    }

    private void ChangeState()
    {
        m_cannonTowerAttack.AlternativeAttack = !m_cannonTowerAttack.AlternativeAttack;
    }
}
