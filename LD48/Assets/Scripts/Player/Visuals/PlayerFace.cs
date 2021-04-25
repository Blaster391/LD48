using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFace : MonoBehaviour
{
    [SerializeField]
    private float m_faceUpdateSpeed = 1.0f;

    [SerializeField]
    private List<Sprite> m_happyFaces = new List<Sprite>();

    [SerializeField]
    private List<Sprite> m_midFaces = new List<Sprite>();

    [SerializeField]
    private List<Sprite> m_sadFaces = new List<Sprite>();

    private float m_lastUpdateTime = 0.0f;
    private int m_index = 0;
    private PlayerHealth m_health = null;
    private SpriteRenderer m_renderer = null;

    void Start()
    {
        m_lastUpdateTime = Random.value;
        m_health = GameMaster.GetPlayer().GetComponent<PlayerHealth>();
        m_renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        m_lastUpdateTime += Time.deltaTime;
        float updateTime = 1 / m_faceUpdateSpeed;

        if (m_lastUpdateTime > updateTime)
        {
            m_lastUpdateTime = 0;
            m_index++;

            if(m_index > 100000000)
            {
                m_index = 0;
            }

            float healthPercentage = (float)m_health.GetCurrentHealth() / m_health.GetMaxHealth();
            if (healthPercentage > 0.75f)
            {
                m_renderer.sprite = m_happyFaces[m_index % m_happyFaces.Count];
            }
            else if (healthPercentage > 0.35f)
            {
                m_renderer.sprite = m_midFaces[m_index % m_midFaces.Count];
            }
            else
            {
                m_renderer.sprite = m_sadFaces[m_index % m_sadFaces.Count];
            }
        }

    }
}
