using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITriggerSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> m_spawnPoints = new List<GameObject>();
    [SerializeField]
    private List<GameObject> m_possibleMonsters = new List<GameObject>();
    [SerializeField]
    private float m_chance = 1.0f;
    [SerializeField]
    private bool m_triggerOnce = true;


    private bool m_triggered = false;

    public void NotifyPlayerEntered()
    {

    }
}
