using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerableAI : MonoBehaviour
{

    public bool Active()
    {
        AIDamage damage = GetComponent<AIDamage>();
        if(damage != null && damage.IsDead())
        {
            return false;
        }

        PlayerHealth playerHealth = GameMaster.GetPlayer()?.GetComponent<PlayerHealth>();
        if(playerHealth != null && playerHealth.IsDead())
        {
            return false;
        }

        PlayerEndGame playerEndGame = GameMaster.GetPlayer()?.GetComponent<PlayerEndGame>();
        if (playerEndGame != null && playerEndGame.IsGameFinished())
        {
            return false;
        }

        return (m_triggerArea == null || m_triggerArea.Active());
    }

    private AITriggerArea m_triggerArea = null;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AITriggerArea triggerArea = collision.GetComponent<AITriggerArea>();
        if (triggerArea != null)
        {
            m_triggerArea = triggerArea;
        }
    }
}
