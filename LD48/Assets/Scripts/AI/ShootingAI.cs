using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAI : MonoBehaviour
{
    [SerializeField]
    private float m_combatRangeMin = 5.0f;
    [SerializeField]
    private float m_combatRangeMax = 5.0f;
    [SerializeField]
    private float m_combatRange = 0.0f;

    [SerializeField]
    private float m_attackSpeed = 1.0f;
    [SerializeField]
    private float m_attackSpeedRand = 1.0f;

    [SerializeField]
    private float m_projectileSpeed = 1.0f;

    [SerializeField]
    private int m_attackDamage = 1;

    [SerializeField]
    private bool m_move = true;

    [SerializeField]
    private bool m_aimAtPlayer = true;
    [SerializeField]
    private float m_inaccuracy = 0.0f;

    [SerializeField]
    private List<Vector2> m_firingDirections = new List<Vector2>();

    private GameObject m_player;
    private TriggerableAI m_triggerableAI;
    private AIMovementHelper m_movement;
    private ShootingHelper m_shooting;
    private Rigidbody2D m_rigidbody;
    private float m_attackCooldown = 0.0f;

    void Start()
    {
        m_player = GameMaster.GetPlayer();
        m_triggerableAI = GetComponent<TriggerableAI>();
        m_movement = GetComponent<AIMovementHelper>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_shooting = GetComponent<ShootingHelper>();

        m_combatRange = Random.Range(m_combatRangeMin, m_combatRangeMax);
        if (m_attackSpeed > 0)
        {
            float attackRate = (1 / m_attackSpeed);
            m_attackCooldown += Random.value * attackRate;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_player != null && m_triggerableAI.Active())
        {
            if(m_move)
            {
                Vector2 playerPositionOffset = transform.position - m_player.transform.position;
                if(playerPositionOffset.magnitude > m_combatRange)
                {
                    m_movement.MoveToPlayer();
                }
            }

            if (m_attackSpeed > 0)
            {
                m_attackCooldown += Time.deltaTime;
   
                float attackRate = (1 / m_attackSpeed);
                if (m_attackCooldown > attackRate)
                {
                    Attack();
                    m_attackCooldown -= attackRate + Random.value * m_attackSpeedRand;
                }
            }
        }
    }

    void Attack()
    {
        if(m_aimAtPlayer)
        {
            Vector2 aimPosition = m_movement.PlayerPosition();
            Vector2 aimDirection =  aimPosition - new Vector2(transform.position.x, transform.position.y);
            aimDirection += MathsHelper.RandomWithNegativeVector2() * m_inaccuracy;
            FireBullet(aimDirection);

        }
        else
        {
            foreach(var aimDirection in m_firingDirections)
            {
                FireBullet(aimDirection);
            }
        }
    }

    void FireBullet(Vector2 _aimDirection)
    {
        if (_aimDirection != Vector2.zero)
        {
            _aimDirection.Normalize();

            Vector2 aimVelocity = _aimDirection * m_projectileSpeed;
            if (m_rigidbody != null)
            {
                aimVelocity += m_rigidbody.velocity;
            }

            m_shooting.Shoot(aimVelocity, m_attackDamage);
        }
    }
}
