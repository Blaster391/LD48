using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyTracker : MonoBehaviour
{
    [SerializeField]
    private GameObject m_keyPrefab;
    [SerializeField]
    private float m_spacing = 50.0f;

    private PlayerKeys m_playerKeys = null;

    private List<GameObject> m_keys = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        m_playerKeys = GameMaster.GetPlayer().GetComponent<PlayerKeys>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_playerKeys != null)
        {
            foreach (var heart in m_keys)
            {
                Destroy(heart);
            }
            m_keys.Clear();


            Vector3 position = transform.position;

            foreach(var k in m_playerKeys.GetKeys())
            { 
                GameObject key = Instantiate<GameObject>(m_keyPrefab, position, Quaternion.identity, transform);
                key.GetComponent<Image>().color = k.m_keyColour;
                m_keys.Add(key);
                position += Vector3.left * m_spacing;
            }
        }
    }
}
