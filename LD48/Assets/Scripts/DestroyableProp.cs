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
    private List<GameObject> m_drops = new List<GameObject>();
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
            if(m_drops.Count > 0)
            {
                if(m_dropChance >= Random.value)
                {
                    Instantiate<GameObject>(m_drops[Random.Range(0, m_drops.Count)], transform.position, Quaternion.identity);
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
