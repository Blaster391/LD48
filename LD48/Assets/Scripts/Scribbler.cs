using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scribbler : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> m_scribbles = new List<GameObject>();
    [SerializeField]
    private float m_timeToScribble = 1.0f;

    [SerializeField]
    private bool m_destroyOnCompletion = false;


    private bool m_scribbling = false;
    private bool m_scribblingComplete = false;
    private float m_scribbleTime = 0.0f;
    private int m_scribbledIndex = 0;
    private Collider2D m_collider = null;

    public void BeginScribble()
    {
        m_scribbling = true;
        if(m_collider != null)
        {
            m_collider.enabled = false;
        }
    }

    public bool ScribblingComplete()
    {
        return m_scribblingComplete;
    }

    private void Start()
    {
        m_collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if(m_scribbling && !m_scribblingComplete)
        {
            m_scribbleTime += Time.deltaTime;
            float timeForEachScribble = m_timeToScribble / m_scribbles.Count;

            if(m_scribbleTime > timeForEachScribble * m_scribbledIndex)
            {
                if(m_scribbledIndex >= m_scribbles.Count)
                {
                    m_scribblingComplete = true;
                    if(m_destroyOnCompletion)
                    {
                        Destroy(gameObject);
                    }

                    return;
                }

                Instantiate<GameObject>(m_scribbles[m_scribbledIndex], transform.position, Quaternion.identity, transform);

                m_scribbledIndex++;
            }
        }
    }

}
