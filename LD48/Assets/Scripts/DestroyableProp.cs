using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableProp : IDamageReceiver
{
    [SerializeField]
    private AudioClip m_hitFX;

    [SerializeField]
    private int m_baseHealth = 1;
    [SerializeField]
    private int m_currentHealth = 1;

    [SerializeField]
    private GameObject m_drop = null;
    [SerializeField]
    private float m_dropChance = 0.0f;

    private Scribbler m_scribbler = null;

    private void Start()
    {
        m_currentHealth = m_baseHealth;

        m_scribbler = GetComponent<Scribbler>();
    }

    public override void ReceiveDamage(int _damage)
    {
        m_currentHealth -= _damage;
        if(m_currentHealth <= 0)
        {
            if(m_drop != null)
            {
                if(m_dropChance >= Random.value)
                {
                    Instantiate<GameObject>(m_drop, transform.position, Quaternion.identity);
                }
            }

            if (m_scribbler != null)
            {
                GameMaster.GetAudioManager().CreateAndPlayAudioObject(m_hitFX);
                m_scribbler.BeginScribble();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }


}
