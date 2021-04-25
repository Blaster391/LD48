using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> m_menuSprites = new List<Sprite>();

    [SerializeField]
    private float m_flickerSpeed = 3.0f;

    private float m_lastUpdateTime = 0.0f;
    private int m_index = 0;
    private SpriteRenderer m_renderer = null;

    void Start()
    {
        m_renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        m_lastUpdateTime += Time.deltaTime;
        float updateTime = 1 / m_flickerSpeed;

        if (m_lastUpdateTime > updateTime)
        {
            m_lastUpdateTime = 0;
            m_index++;

            if (m_index > 100000000)
            {
                m_index = 0;
            }


            m_renderer.sprite = m_menuSprites[m_index % m_menuSprites.Count];
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
    }
}
