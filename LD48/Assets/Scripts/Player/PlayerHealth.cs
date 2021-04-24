using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : IDamageReceiver
{
    [SerializeField]
    private int m_baseHealth = 3;
    [SerializeField]
    private int m_maxHealth;
    [SerializeField]
    private int m_currentHealth;
    [SerializeField]
    private float m_iFrameTime = 0.5f;

    private float m_iFramesRemaining = 0.0f;

    [SerializeField]
    private GameObject m_gameOverScreen = null;

    private Scribbler m_scribbler;

    public override void ReceiveDamage(int _damage)
    {
        if(IsInvincible())
        {
            return;
        }

        m_currentHealth -= _damage;
        if(m_currentHealth <= 0)
        {
            m_scribbler.BeginScribble();
        }
        else
        {
            m_iFramesRemaining = m_iFrameTime;
        }
    }

    public void IncrementCurrentHealth()
    {
        if(m_currentHealth < m_maxHealth)
        {
            m_currentHealth++;
        }
    }

    public bool IsDead()
    {
        return (m_baseHealth <= 0);
    }

    public bool IsInvincible()
    {
        return m_iFramesRemaining > 0.0f;
    }

    public int GetCurrentHealth()
    {
        return m_currentHealth;
    }

    public int GetMaxHealth()
    {
        return m_maxHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_maxHealth = m_baseHealth;
        m_currentHealth = m_baseHealth;
        m_scribbler = GetComponent<Scribbler>();
    }
    private void Update()
    {
        if(IsInvincible())
        {
            m_iFramesRemaining -= Time.deltaTime;
        }

        if(m_scribbler.ScribblingComplete())
        {
            m_gameOverScreen.SetActive(true);

            if(Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(0, LoadSceneMode.Single);
            }
        }
    }

    public void PowerDownHealth()
    {
        m_maxHealth--;
        if(m_currentHealth > m_maxHealth)
        {
            m_currentHealth = m_maxHealth;
            if(m_maxHealth == 0)
            {
                m_scribbler.BeginScribble();
            }
        }
    }

    public void PowerUpHealth()
    {
        m_maxHealth++;
    }
}
