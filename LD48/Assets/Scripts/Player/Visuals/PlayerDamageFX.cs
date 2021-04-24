using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageFX : MonoBehaviour
{
    [SerializeField]
    private float m_strobeTime = 0.2f;
    [SerializeField]
    private Color m_strobeColour;

    private Color m_originalColour;
    private SpriteRenderer m_renderer;
    private PlayerHealth m_health;
    private GameObject m_player;
    private float m_currentStrobeTime = 0.0f;
    private bool m_strobeUp = true;

    void Start()
    {
        m_player = GameMaster.GetPlayer();
        m_health = m_player.GetComponent<PlayerHealth>();

        m_renderer = GetComponent<SpriteRenderer>();
        m_originalColour = m_renderer.color;
    }

    // Update is called once per frame
    void Update()
    {


        if (m_health.IsInvincible() && m_strobeUp)
        {
            m_renderer.color = m_strobeColour;
            m_currentStrobeTime += Time.deltaTime;
            m_strobeUp = m_currentStrobeTime < m_strobeTime;
        }
        else if(m_health.IsInvincible() && !m_strobeUp)
        {
            m_renderer.color = m_originalColour;
            m_currentStrobeTime -= Time.deltaTime;
            m_strobeUp = m_currentStrobeTime < 0.0f;
        }
        else
        {
            m_renderer.color = m_originalColour;
            m_currentStrobeTime = 0.0f;
        }
    }
}
