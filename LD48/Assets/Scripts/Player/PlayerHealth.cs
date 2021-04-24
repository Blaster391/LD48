using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : IDamageReceiver
{
    [SerializeField]
    private int m_baseHealth = 3;
    [SerializeField]
    private int m_maxHealth;
    [SerializeField]
    private int m_currentHealth;

    public override void ReceiveDamage(int _damage)
    {
        m_currentHealth -= _damage;
        if(m_baseHealth <= 0)
        {
            // GAME OVER MAN
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_maxHealth = m_baseHealth;
        m_currentHealth = m_baseHealth;
    }
}
