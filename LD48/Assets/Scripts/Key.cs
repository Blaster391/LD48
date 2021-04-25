using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    private AudioClip m_pickupFX;

    [SerializeField]
    private string m_id;
    [SerializeField]
    private Color m_color;

    private void Start()
    {
        GetComponent<SpriteRenderer>().color = m_color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameMaster.GetPlayer())
        {
            if(m_pickupFX != null)
            {
                GameMaster.GetAudioManager().CreateAndPlayAudioObject(m_pickupFX);
            }

            PlayerKeys playerKeys = collision.gameObject.GetComponent<PlayerKeys>();
            playerKeys.RegisterKey(m_id, m_color);
            Destroy(gameObject);
        }
    }
    public string GetId()
    {
        return m_id;
    }

    public Color GetColour()
    {
        return m_color;
    }
}
