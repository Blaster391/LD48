using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDamage : IDamageReceiver
{
    [SerializeField]
    private int m_health = 3;

    private Scribbler m_scribbler;
    private BossAI m_bossAI;
    private void Start()
    {
        m_scribbler = GetComponent<Scribbler>();
        m_bossAI = GetComponent<BossAI>();
    }

    public override void ReceiveDamage(int _damage)
    {
        m_health -= _damage;
        if(IsDead())
        {
            if(m_bossAI)
            {
                m_bossAI.Die();
            }
            if(m_scribbler != null)
            {
                m_scribbler.BeginScribble();
            }
            else
            {

                Destroy(gameObject);
            }
        }
    }

    public bool IsDead()
    {
        return m_health <= 0;
    }

    public int GetHealth()
    {
        return m_health;
    }

    private void Update()
    {
        if (m_scribbler != null && m_scribbler.ScribblingComplete())
        {
            Destroy(gameObject);
        }
    }
}
