using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    [SerializeField]
    private string m_endGameKeyId;

    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameMaster.GetPlayer())
        {
            PlayerKeys playerKeys = collision.gameObject.GetComponent<PlayerKeys>();
            if (playerKeys.HasKey(m_endGameKeyId))
            {
                collision.gameObject.GetComponent<PlayerEndGame>().CompleteGame();

                playerKeys.RemoveKey(m_endGameKeyId);
                GetComponent<Fader>().StartFade();
            }
        }
    }
}
