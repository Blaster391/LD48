using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerObject : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> m_flickerSprites = new List<Sprite>();

    int m_randomOffset = 0;

    private SpriteRenderer m_spriteRenderer;
    private FlickerManager m_flickerManager;
    private void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_flickerManager = GameMaster.GetFlickerManager();

        m_randomOffset = Random.Range(0, m_flickerSprites.Count);
    }


    // Update is called once per frame
    void Update()
    {
        m_spriteRenderer.sprite = m_flickerSprites[m_flickerManager.GetFlickerIndex() % m_flickerSprites.Count];
    }
}
