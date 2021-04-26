using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private float m_velocityMod = 1.0f;

    [SerializeField]
    private float m_baseInaccuracy = 0.01f;
    [SerializeField]
    private float m_baseAttackSpeed = 1.0f;
    [SerializeField]
    private int m_baseAttackDamage = 1;
    [SerializeField]
    private float m_baseProjectileSpeed = 1.0f;

    [SerializeField]
    private float m_currentAttackSpeed = 0.0f;
    [SerializeField]
    private int m_currentAttackDamage = 0;
    [SerializeField]
    private float m_currentProjectileSpeed = 0;
    [SerializeField]
    private float m_currentInaccuracy = 0.0f;
    [SerializeField]
    private int m_currentAttackCount = 1;

    [SerializeField]
    private float m_attackAccuracyPowerUpMod = 0.5f;
    [SerializeField]
    private float m_attackSpeedPowerUpMod = 1.0f;

    private AudioCharacterPlayer m_audio;
    private PlayerControls m_controls;
    private PlayerMovement m_movement;
    private ShootingHelper m_shoot;
    private PlayerEndGame m_endGame;
    private PlayerHealth m_health;

    private float m_attackCooldown = 0.0f;

    void Start()
    {
        m_audio = GetComponent<AudioCharacterPlayer>();
        m_controls = GetComponent<PlayerControls>();
        m_movement = GetComponent<PlayerMovement>();
        m_shoot = GetComponent<ShootingHelper>();
        m_endGame = GetComponent<PlayerEndGame>();
        m_health = GetComponent<PlayerHealth>();

        m_currentAttackSpeed = m_baseAttackSpeed;
        m_currentAttackDamage = m_baseAttackDamage;
        m_currentProjectileSpeed = m_baseProjectileSpeed;
        m_currentInaccuracy = m_baseInaccuracy;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_endGame.IsGameFinished() || m_health.IsDead())
        {
            return;
        }

        if (m_currentAttackSpeed > 0)
        {
            m_attackCooldown += Time.deltaTime;
            if (m_controls.AttackPressed())
            {
                float attackRate = (1 / m_currentAttackSpeed);
                if (m_attackCooldown > attackRate)
                {
                    Attack();
                    m_attackCooldown = 0.0f;
                }
            }
        }
    }

    internal void PowerUpCount()
    {
        m_currentAttackCount++;
    }

    internal void PowerUpSpeed()
    {
        m_currentAttackSpeed += m_attackSpeedPowerUpMod;
    }

    internal void PowerUpAccuracy()
    {
        m_currentInaccuracy *= m_attackAccuracyPowerUpMod;
    }

    internal void PowerUpDamage()
    {
        m_currentAttackDamage++;
    }

    void Attack()
    {
        m_audio.TriggerPlayerAttack();

        Vector2 attackDirection = GetAttackDirection();
        for(int i = 0; i < m_currentAttackCount; ++i)
        {
            Vector2 attackNormal = attackDirection.PerpendicularClockwise();
            Vector2 inaccuracy = attackNormal * MathsHelper.RandomWithNegative() * (m_currentInaccuracy * (i + 1));
            Vector2 attackVelocity = (attackDirection + inaccuracy) * (m_currentProjectileSpeed);

            Vector2 modifiedVelocity = attackVelocity + (m_movement.CurrentVelocity() * m_velocityMod);

            if(attackVelocity.magnitude < modifiedVelocity.magnitude)
            {
                attackVelocity = modifiedVelocity;
            }

            m_shoot.Shoot(attackVelocity, m_currentAttackDamage);
        }
    }

    internal void PowerDownDamage()
    {
        m_currentAttackDamage--;
        if(m_currentAttackDamage < 1)
        {
            m_currentAttackDamage = 1;
        }
    }

    internal void PowerDownSpeed()
    {
        m_currentAttackSpeed -= m_attackSpeedPowerUpMod;
        if(m_currentAttackSpeed < m_baseAttackSpeed)
        {
            m_currentAttackSpeed = m_baseAttackSpeed;
        }
    }

    internal void PowerDownAccuracy()
    {
        m_currentInaccuracy /= m_attackAccuracyPowerUpMod;
    }

    public Vector2 GetAttackDirection()
    {
        Vector2 direction = new Vector2(0, 0);

        bool upPressed = m_controls.AttackUpPressed();
        bool downPressed = m_controls.AttackDownPressed();
        bool rightPressed = m_controls.AttackRightPressed();
        bool leftPressed = m_controls.AttackLeftPressed();

        if (upPressed)
        {
            direction += Vector2.up;
        }
        else if (downPressed)
        {
            direction += Vector2.down;
        }

        if (rightPressed)
        {
            direction += Vector2.right;
        }
        else if (leftPressed)
        {
            direction += Vector2.left;
        }

        if(direction != Vector2.zero)
        {
            direction.Normalize();
        }

        return direction;
    }
}
