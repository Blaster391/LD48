using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private int m_min = 1;
    [SerializeField]
    private int m_max = 1;

    [SerializeField]
    private bool m_triggerOnce = true;

    private List<GameObject> m_availableSpawns = new List<GameObject>();
    private List<GameObject> m_spawnedMonsters = new List<GameObject>();
    private bool m_triggered = false;

    public void NotifyPlayerEntered()
    {
        if (!m_triggered || !m_triggerOnce)
        {
            if (!SpawnedMonstersStillLive() && (m_chance >= Random.value))
            {
                m_triggered = true;
                Spawn();
            }
        }
    }

    public bool SpawnedMonstersStillLive()
    {
        foreach(GameObject monster in m_spawnedMonsters)
        {
            if(monster != null)
            {
                return true;
            }
        }

        return false;
    }

    public void Spawn()
    {
        if(m_possibleMonsters.Count == 0)
        {
            Debug.LogError("No monsters for spawner");
            return;
        }

        m_availableSpawns = m_spawnPoints.ToList();
        m_spawnedMonsters.Clear();

        int numberToSpawn = Random.Range(m_min, m_max + 1);
        for(int i = 0; i < numberToSpawn; ++i)
        {
            int monsterIndex = Random.Range(0, m_possibleMonsters.Count);
            Vector3 spawnPosition = transform.position;

            if(m_availableSpawns.Count > 0)
            {
                int spawnIndex = Random.Range(0, m_availableSpawns.Count);
                spawnPosition = m_availableSpawns[spawnIndex].transform.position;
                m_availableSpawns.RemoveAt(spawnIndex);
            }

            GameObject monster = Instantiate(m_possibleMonsters[monsterIndex], spawnPosition, Quaternion.identity);
            m_spawnedMonsters.Add(monster);

        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameMaster.GetPlayer())
        {
            NotifyPlayerEntered();
        }
    }

    public void KillAllAI()
    {
        foreach (GameObject monster in m_spawnedMonsters)
        {
            Destroy(monster);
        }
    }
}
