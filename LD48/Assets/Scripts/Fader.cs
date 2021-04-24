using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    [SerializeField]
    private bool m_fadeInOnSpawn = false;
    [SerializeField]
    private float m_timeToFade = 1.0f;
    [SerializeField]
    private bool m_manageExternally = false;
    [SerializeField]
    private bool m_dieOnFade = false;

    private SpriteRenderer m_renderer = null;
    private float m_fadeAmount = 0.0f;
    private bool m_fade = false;

    void Start()
    {
        m_renderer = GetComponent<SpriteRenderer>();

        if(m_timeToFade <= 0.0f)
        {
            m_timeToFade = 0.01f;
        }

        if(m_fadeInOnSpawn)
        {
            SetFade(1.0f);
        }
        else
        {
            SetFade(0.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!m_manageExternally)
        {
            if (m_fade && m_fadeAmount < 1.0f)
            {
                float fadeStep = 1 / m_timeToFade;
                m_fadeAmount += fadeStep * Time.deltaTime;
                SetFade(m_fadeAmount);
            }
            else if(!m_fade && m_fadeAmount > 0.0f)
            {
                float fadeStep = 1 / m_timeToFade;
                m_fadeAmount -= fadeStep * Time.deltaTime;
                SetFade(m_fadeAmount);
            }


            if(m_fade && m_dieOnFade && m_fadeAmount >= 1.0f)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetFade(float _fade)
    {
        m_fadeAmount = _fade;

        float alpha = 1.0f - _fade;
        Color colour = m_renderer.color;
        colour.a = alpha;
        m_renderer.color = colour;
    }

    public void StartFade()
    {
        m_fade = true;
        if(m_dieOnFade)
        {
            Collider2D collider = GetComponent<Collider2D>();
            if(collider != null)
            {
                collider.enabled = false;
            }
        }
    }
}
