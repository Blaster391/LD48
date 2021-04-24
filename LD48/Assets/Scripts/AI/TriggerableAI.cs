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
