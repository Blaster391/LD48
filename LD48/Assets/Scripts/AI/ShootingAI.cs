using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAI : MonoBehaviour
{
    private GameObject m_player;
    private TriggerableAI m_triggerableAI;
    private AIMovementHelper m_movement;

    void Start()
    {
        m_player = GameMaster.GetPlayer();
        m_triggerableAI = GetComponent<TriggerableAI>();
        m_movement = GetComponent<AIMovementHelper>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
