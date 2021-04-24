using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovementHelper : MonoBehaviour
{
    [SerializeField]
    private float m_speed = 10.0f;
    [SerializeField]
    private float m_accelerationRate = 1000.0f;

    private Rigidbody2D m_rigidBody;

    private void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
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
        Vector2 direction =  _target - myPos;
        if(direction == Vector2.zero)
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

    public void MoveToTarget(Vector2 _position)
    {
        Vector2 movementDirection = DirectionToPosition(_position);
        if(movementDirection == Vector2.zero)
        {
            return;
        }

        m_rigidBody.AddForce(movementDirection * m_accelerationRate * m_speed * Time.deltaTime);

        if (m_rigidBody.velocity.magnitude > m_speed)
        {
            Vector2 velocity = m_rigidBody.velocity;
            velocity.Normalize();
            m_rigidBody.velocity = velocity * m_speed;
        }
    }
}
