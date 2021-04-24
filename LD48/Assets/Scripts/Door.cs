using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private string m_id;

    void Start()
    {
        var keys = GameObject.FindObjectsOfType<Key>(true);
        foreach (var key in keys)
        {
            if (key.GetId() == m_id)
            {
                GetComponent<SpriteRenderer>().color = key.GetColour();
                return;
            }
        }

        Debug.LogError($"Door has no key {m_id}");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameMaster.GetPlayer())
        {
            PlayerKeys playerKeys = collision.gameObject.GetComponent<PlayerKeys>();
            if(playerKeys.HasKey(m_id))
            {
                playerKeys.RemoveKey(m_id);
                GetComponent<Fader>().StartFade();
            }
        }
    }
}
