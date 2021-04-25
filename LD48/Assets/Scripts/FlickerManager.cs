using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerManager : MonoBehaviour
{
    [SerializeField]
    private float m_flickerSpeed = 1.0f;

    private int m_flickerIndex = 0;
    private float m_timeSinceFlicker = 0.0f;


    void Awake()
    {
        GameMaster.RegisterFlickerManager(this);
    }

    // Update is called once per frame
    void Update()
    {
        m_timeSinceFlicker += Time.deltaTime;
        float flickerTime = 1 / m_flickerSpeed;

        if(m_timeSinceFlicker > flickerTime)
        {
            m_timeSinceFlicker = 0;
            m_flickerIndex++;
            if(m_flickerIndex > 100000000)
            {
                m_flickerIndex = 0;
            }
        }
    }

    public int GetFlickerIndex()
    {
        return m_flickerIndex;
    }
}
