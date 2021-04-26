using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITriggerArea : MonoBehaviour
{
    private AITriggerSpawner m_spawner = null;
    private bool m_active = false;
    public bool Active()
    {
        return m_active;
    }

    private void Awake()
    {
        m_spawner = GetComponent<AITriggerSpawner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameMaster.GetPlayer())
        {
            m_active = true;
        }

        if(!m_active)
        {
            if (collision.gameObject.GetComponent<Bullet>() != null)
            {
                Destroy(collision.gameObject);
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == GameMaster.GetPlayer())
        {
            m_active = false;
        }
    }
}
