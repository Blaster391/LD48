using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    [SerializeField]
    private GameObject m_heartFull;
    [SerializeField]
    private GameObject m_heartEmpty;

    [SerializeField]
    private float m_spacing = 1.0f;

    private PlayerHealth m_health = null;

    private List<GameObject> m_hearts = new List<GameObject>();

    private void Start()
    {
        m_health = GameMaster.GetPlayer().GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if(m_health != null)
        {
            foreach(var heart in m_hearts)
            {
                Destroy(heart);
            }
            m_hearts.Clear();


            Vector3 position = transform.position;
            int currentHealth = m_health.GetCurrentHealth();

            for(int i = 0; i < m_health.GetMaxHealth(); ++i)
            {
                GameObject heart = null;
                if(i < currentHealth)
                {
                    heart = Instantiate<GameObject>(m_heartFull, position, Quaternion.identity, transform);
                }
                else
                {
                    heart = Instantiate<GameObject>(m_heartEmpty, position, Quaternion.identity, transform);
                }
                m_hearts.Add(heart);

                position += Vector3.left * m_spacing;
            }
        }
    }
}
