using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDamage : IDamageReceiver
{
    [SerializeField]
    private int m_health = 3;

    private Scribbler m_scribbler;
    private void Start()
    {
        m_scribbler = GetComponent<Scribbler>();
    }

    public override void ReceiveDamage(int _damage)
    {
        m_health -= _damage;
        if(IsDead())
        {
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

    private void Update()
    {
        if (m_scribbler != null && m_scribbler.ScribblingComplete())
        {
            Destroy(gameObject);
        }
    }
}
