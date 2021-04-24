using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float m_accelerationRate = 1.0f;


    [SerializeField]
    private float m_baseMovementSpeed = 1.0f;

    private Rigidbody2D m_rigidBody;
    private PlayerControls m_controls;

    [SerializeField]
    private float m_currentMovementSpeed;

    void Awake()
    {
        GameMaster.RegisterPlayer(gameObject);
    }

    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_controls = GetComponent<PlayerControls>();
        m_currentMovementSpeed = m_baseMovementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movementDirection = DesiredMovementDirection();
        m_rigidBody.AddForce(movementDirection * m_accelerationRate * m_currentMovementSpeed * Time.deltaTime);

        if(m_rigidBody.velocity.magnitude > m_currentMovementSpeed)
        {
            Vector2 velocity = m_rigidBody.velocity;
            velocity.Normalize();
            m_rigidBody.velocity = velocity * m_currentMovementSpeed;
        }
    }

    public Vector2 DesiredMovementDirection()
    {
        Vector2 movement = new Vector2(0, 0);

        bool upPressed = m_controls.UpPressed();
        bool downPressed = m_controls.DownPressed();
        bool rightPressed = m_controls.RightPressed();
        bool leftPressed = m_controls.LeftPressed();

        if (upPressed)
        {
            movement += Vector2.up;
        }

        if (downPressed)
        {
            movement += Vector2.down;
        }

        if (rightPressed)
        {
            movement += Vector2.right;
        }

        if (leftPressed)
        {
            movement += Vector2.left;
        }

        return movement;
    }

}
