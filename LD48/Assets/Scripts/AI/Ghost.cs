using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField]
    private float m_lifespan = 10.0f;
    [SerializeField]
    private int m_damage = 1;


    private float m_age = 0.0f;

    private GameObject m_player;
    private TriggerableAI m_triggerableAI;
    private AIMovementHelper m_movement;

    void Start()
    {
        m_player = GameMaster.GetPlayer();
        m_triggerableAI = GetComponent<TriggerableAI>();
        m_movement = GetComponent<AIMovementHelper>();
    }

    void Update()
    {
        if(m_player != null && m_triggerableAI.Active())
        {
            m_age += Time.deltaTime;
            if(m_age > m_lifespan)
            {
                Destroy(gameObject);
            }

            m_movement.MoveToPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == m_player)
        {
            OnHitPlayer();
        }
    }

    private void OnHitPlayer()
    {
        IDamageReceiver playerDamage = m_player.GetComponent<IDamageReceiver>();
        playerDamage.ReceiveDamage(m_damage);
        Destroy(gameObject);
    }
}
