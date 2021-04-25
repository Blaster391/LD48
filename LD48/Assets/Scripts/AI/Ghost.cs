using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField]
    private float m_lifespan = 10.0f;


    private float m_age = 0.0f;

    private GameObject m_player;
    private TriggerableAI m_triggerableAI;
    private Fader m_fader;

    void Start()
    {
        m_player = GameMaster.GetPlayer();
        m_triggerableAI = GetComponent<TriggerableAI>();
        m_fader = GetComponent<Fader>();

        GameMaster.GetAudioManager().TriggerGhostSound();
    }

    void Update()
    {
        if(m_player != null && m_triggerableAI.Active())
        {
            m_age += Time.deltaTime;

            m_fader.SetFade(m_age / m_lifespan);

            if (m_age > m_lifespan)
            {
                Destroy(gameObject);
            }
        }
    }
}
