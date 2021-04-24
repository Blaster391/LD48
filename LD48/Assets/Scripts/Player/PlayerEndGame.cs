using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEndGame : MonoBehaviour
{
    [SerializeField]
    private GameObject m_endGamePoint;
    [SerializeField]
    private float m_endGameLerpTime = 10.0f;
    [SerializeField]
    private float m_endGameMusicStartTime = 5.0f;
    [SerializeField]
    private float m_endGameCameraColourFadeTime = 5.0f;
    [SerializeField]
    private float m_endGameWhiteoutTime = 5.0f;
    [SerializeField]
    private Image m_gameEndPanel;
    [SerializeField]
    private GameObject m_gameEndScreen;
    [SerializeField]
    private Color m_endSkyboxColour;

    private bool m_finished = false;
    private bool m_musicStarted = false;
    private float m_progress = 0.0f;
    private Vector3 m_startPosition;
    private Color m_skyboxStartColour;
    private Camera m_camera;
    private Rigidbody2D m_rigidBody;

    public bool IsGameFinished()
    {
        return m_finished;
    }

    public void CompleteGame()
    {
        m_finished = true;
        m_startPosition = GameMaster.GetPlayer().transform.position;
        m_skyboxStartColour = m_camera.backgroundColor;

        if (m_rigidBody != null)
        {
            m_rigidBody.velocity = Vector2.zero;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_camera = GameMaster.GetPlayerCamera().gameObject.GetComponent<Camera>();
        m_rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_finished)
        {
            m_progress += Time.deltaTime;

            if(m_progress > m_endGameMusicStartTime && !m_musicStarted)
            {
                m_musicStarted = true;
                GameMaster.GetAudioManager().PlayEndGameTrack();
            }

            transform.position = Vector3.Lerp(m_startPosition, m_endGamePoint.transform.position, m_progress / m_endGameLerpTime);

            float skyboxLerp = m_progress / m_endGameCameraColourFadeTime;
            m_camera.backgroundColor = Color.Lerp(m_skyboxStartColour, m_endSkyboxColour, skyboxLerp);


            float whiteoutLerp = (m_progress - m_endGameLerpTime) / m_endGameWhiteoutTime;
            if (whiteoutLerp > 0.0f)
            {
                m_gameEndPanel.gameObject.SetActive(true);
                Color colour = m_gameEndPanel.color;
                colour.a = whiteoutLerp;
                m_gameEndPanel.color = colour;


                if(whiteoutLerp > 1.5f)
                {
                    m_gameEndScreen.SetActive(true);
                }
            }
        }
    }
}
