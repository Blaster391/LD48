using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMChangeTrigger : MonoBehaviour
{
    [SerializeField]
    private int m_stage = 0;
    [SerializeField]
    private bool m_triggerBossBuildup = false;
    [SerializeField]
    private bool m_triggerBossBoss = false;

    private bool m_done = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(m_done)
        {
            return;
        }

        if (collision.gameObject == GameMaster.GetPlayer())
        {
            if(m_triggerBossBuildup)
            {
                m_done = true;
                GameMaster.GetAudioManager().StartBossBuildupMusic();
            }
            else if(m_triggerBossBoss)
            {
                m_done = true;
                GameMaster.GetAudioManager().StartBossMusic();
            }
            else
            {
                GameMaster.GetAudioManager().StartBGMPhase(m_stage);
            }
        }
    }
}
