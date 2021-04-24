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
    private GameObject m_gameOverScreen = null;

    private Scribbler m_scribbler;

    public override void ReceiveDamage(int _damage)
    {
        m_currentHealth -= _damage;
        if(m_currentHealth <= 0)
        {
            m_scribbler.BeginScribble();
        }
    }

    public bool IsDead()
    {
        return (m_baseHealth <= 0);
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
        if(m_scribbler.ScribblingComplete())
        {
            m_gameOverScreen.SetActive(true);

            if(Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(0, LoadSceneMode.Single);
            }
        }
    }
}
