using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDamage : IDamageReceiver
{
    [SerializeField]
    private int m_health = 3;

    public override void ReceiveDamage(int _damage)
    {
        m_health -= _damage;
        if(IsDead())
        {
            Destroy(gameObject);
        }
    }

    public bool IsDead()
    {
        return m_health <= 0;
    }
}
