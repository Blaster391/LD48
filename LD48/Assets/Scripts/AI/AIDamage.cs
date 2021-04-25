using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDamage : IDamageReceiver
{
    [SerializeField]
    private int m_health = 3;

    private Scribbler m_scribbler;
    private BossAI m_bossAI;

    private AudioCharacterBoss m_bossAudio;
    private AudioCharacterEnemy m_enemyAudio;

    private void Start()
    {
        m_scribbler = GetComponent<Scribbler>();
        m_bossAI = GetComponent<BossAI>();

        m_bossAudio = GetComponent<AudioCharacterBoss>();
        m_enemyAudio = GetComponent<AudioCharacterEnemy>();
    }

    public override void ReceiveDamage(int _damage)
    {
        if(m_enemyAudio != null)
        {
            m_enemyAudio.TriggerEnemyDamaged();
        }

        m_health -= _damage;
        if(IsDead())
        {
            if(m_bossAI)
            {
                m_bossAI.Die();
                return;
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
        else if(m_bossAudio != null)
        {
            m_bossAudio.TriggerDamaged();
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
