﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float m_lifetime = 30.0f;
    private float m_age = 0.0f;

    private Rigidbody2D m_rigidbody;
    private GameObject m_firer = null;
    private int m_damage = 0;

    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        m_age += Time.deltaTime;
        if(m_age > m_lifetime)
        {
            Destroy(this);
        }
    }

    public void SetupBullet(Vector2 _velocity, GameObject _firer, int _damage)
    {
        m_firer = _firer;
        m_rigidbody.velocity = _velocity;
        m_damage = _damage;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject != m_firer)
        {
            IDamageReceiver damageReceiver = collider.gameObject.GetComponent<IDamageReceiver>();
            if (damageReceiver != null)
            {
                damageReceiver.ReceiveDamage(m_damage);
            }

            Destroy(gameObject);
        }
    }
}
