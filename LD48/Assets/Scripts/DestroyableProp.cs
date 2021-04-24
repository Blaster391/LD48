using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableProp : IDamageReceiver
{
    [SerializeField]
    private int m_baseHealth = 1;
    [SerializeField]
    private int m_currentHealth = 1;

    private void Start()
    {
        m_currentHealth = m_baseHealth;
    }

    public override void ReceiveDamage(int _damage)
    {
        m_currentHealth -= _damage;
        if(m_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
