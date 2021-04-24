using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerups : MonoBehaviour
{
    [SerializeField]
    private GameObject m_powerUpTextPos;

    [SerializeField]
    private GameObject m_healthUpText;
    [SerializeField]
    private GameObject m_speedUpText;
    [SerializeField]
    private GameObject m_attackDmgUpText;
    [SerializeField]
    private GameObject m_attackSpeedUpText;
    [SerializeField]
    private GameObject m_attackCountUpText;
    [SerializeField]
    private GameObject m_accuracyUpText;
    [SerializeField]
    private GameObject m_healthDownText;
    [SerializeField]
    private GameObject m_speedDownText;
    [SerializeField]
    private GameObject m_attackDamageDown;
    [SerializeField]
    private GameObject m_attackSpeedDownText;
    [SerializeField]
    private GameObject m_accuracyDownText;

    private PlayerMovement m_movement;
    private PlayerHealth m_health;
    private PlayerAttack m_attack;

    void Start()
    {
        m_health = GetComponent<PlayerHealth>();
        m_movement = GetComponent<PlayerMovement>();
        m_attack = GetComponent<PlayerAttack>();
    }

    public void HandlePowerup(PowerUpTypes _powerup)
    {
        GameObject textToCreate = null;

        switch (_powerup)
        {
            case PowerUpTypes.Health:
                textToCreate = m_healthUpText;
                m_health.PowerUpHealth();
                break;
            case PowerUpTypes.Speed:
                textToCreate = m_speedUpText;
                m_movement.PowerUpSpeed();
                break;
            case PowerUpTypes.AttackDamage:
                m_attack.PowerUpDamage();
                textToCreate = m_attackDmgUpText;
                break;
            case PowerUpTypes.AttackSpeed:
                m_attack.PowerUpSpeed();
                textToCreate = m_attackSpeedUpText;
                break;
            case PowerUpTypes.AttackCount:
                m_attack.PowerUpCount();
                textToCreate = m_attackCountUpText;
                break;
            case PowerUpTypes.Accuracy:
                m_attack.PowerUpAccuracy();
                textToCreate = m_accuracyUpText;
                break;
            case PowerUpTypes.HealthDown:
                textToCreate = m_healthDownText;
                m_health.PowerDownHealth();
                break;
            case PowerUpTypes.SpeedDown:
                textToCreate = m_speedDownText;
                m_movement.PowerDownSpeed();
                break;
            case PowerUpTypes.AttackDamageDown:
                m_attack.PowerDownDamage();
                textToCreate = m_attackDamageDown;
                break;
            case PowerUpTypes.AttackSpeedDown:
                m_attack.PowerDownSpeed();
                textToCreate = m_attackSpeedDownText;
                break;
            case PowerUpTypes.AccuracyDown:
                m_attack.PowerDownAccuracy();
                textToCreate = m_accuracyDownText;
                break;
            default:
                Debug.LogError("Unhandled Powerup");
                break;
        }

        if(textToCreate != null)
        {
            Instantiate<GameObject>(textToCreate, m_powerUpTextPos.transform.position, Quaternion.identity, m_powerUpTextPos.transform);
        }
    }
}