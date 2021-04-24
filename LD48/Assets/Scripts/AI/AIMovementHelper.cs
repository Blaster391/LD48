using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovementHelper : MonoBehaviour
{
    [SerializeField]
    private float m_speed = 10.0f;
    [SerializeField]
    private float m_accelerationRate = 1000.0f;
    [SerializeField]
    private float m_wanderSpeedMod = 0.5f;

    [SerializeField]
    private bool m_wanderWhenInactive = false;
    [SerializeField]
    private bool m_wanderAlways = false;

    private Rigidbody2D m_rigidBody;
    private TriggerableAI m_triggerableAI;
    private AIDamage m_damage;
    private Vector2 m_wanderTarget = Vector2.zero;

    private void Awake()
    {
        m_damage = GetComponent<AIDamage>();
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_triggerableAI = GetComponent<TriggerableAI>();
    }

    public Vector2 DirectionToPlayer()
    {
        GameObject player = GameMaster.GetPlayer();

        if (player != null)
        {
            return DirectionToPosition(GameMaster.GetPlayer().transform.position);
        }


        return Vector2.zero;
    }

    public Vector2 PlayerPosition()
    {
        GameObject player = GameMaster.GetPlayer();

        if (player != null)
        {
            return GameMaster.GetPlayer().transform.position;
        }

        return Vector2.zero;
    }


    public Vector2 DirectionToPosition(Vector2 _target)
    {
        Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 direction = _target - myPos;
        if (direction == Vector2.zero)
        {
            return Vector2.zero;
        }

        return direction.normalized;
    }

    public void MoveToPlayer()
    {
        GameObject player = GameMaster.GetPlayer();
        if (player != null)
        {
            MoveToTarget(GameMaster.GetPlayer().transform.position);
        }
    }

    public void MoveToTarget(Vector2 _position, float _speedMod = 1.0f)
    {
        Vector2 movementDirection = DirectionToPosition(_position);
        if (movementDirection == Vector2.zero)
        {
            return;
        }

        float speed = m_speed * _speedMod;

        m_rigidBody.AddForce(movementDirection * m_accelerationRate * speed * Time.deltaTime);

        if (m_rigidBody.velocity.magnitude > speed)
        {
            Vector2 velocity = m_rigidBody.velocity;
            velocity.Normalize();
            m_rigidBody.velocity = velocity * speed;
        }
    }


    private void Update()
    {
        if (m_damage != null && m_damage.IsDead())
        {
            return;
        }

        if (m_wanderAlways || (m_wanderWhenInactive && !m_triggerableAI.Active()))
        {
            if(m_wanderTarget == Vector2.zero)
            {
                RandomWanderTarget(); 
            }

            float distanceToPosition = (new Vector2(transform.position.x, transform.position.y) - m_wanderTarget).magnitude;
            if(distanceToPosition < 0.5f)
            {
                RandomWanderTarget();
            }

            MoveToTarget(m_wanderTarget, m_wanderSpeedMod);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        RandomWanderTarget();
    }

    private void RandomWanderTarget()
    {
        if (m_triggerableAI.GetTriggerArea() != null)
        {
            Collider2D triggerArea = m_triggerableAI.GetTriggerArea().GetComponent<Collider2D>();
            m_wanderTarget = triggerArea.bounds.center;
            m_wanderTarget += MathsHelper.RandomWithNegativeVector2() * triggerArea.bounds.extents;
        }
    }

}
