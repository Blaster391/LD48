using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private float m_cameraHeight = 10.0f;

    [SerializeField]
    private float m_lerpToTriggerTime = 1.0f;

    [SerializeField]
    private float m_shakeLerp = 0.25f;
    [SerializeField]
    private float m_shakeDistance = 0.25f;

    private GameObject m_player;
    private PlayerCameraTriggerArea m_triggerArea = null;
    private float m_lerpAmount = 1.0f;
    private Vector3 m_lerpPosition;
    private bool m_shaking = false;
    private Vector3 m_shakePositionOffset = Vector3.zero;
    private Vector3 m_lastShakePositionOffset = Vector3.zero;
    private float m_shakeTime = 0.0f;

    private void Awake()
    {
        GameMaster.RegisterPlayerCamerea(this);
    }

    void Start()
    {
        m_player = GameMaster.GetPlayer();

        transform.position = GetDesiredCameraPosition();
        m_lerpPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredPosition = GetDesiredCameraPosition();
        Vector3 actualPosition = desiredPosition;

        if(m_lerpAmount < 1.0f && m_lerpToTriggerTime > 0)
        {
            m_lerpAmount += (Time.deltaTime / m_lerpToTriggerTime);
            actualPosition = Vector3.Lerp(m_lerpPosition, desiredPosition, m_lerpAmount);
        }

        if(m_shaking)
        {
            m_shakeTime += Time.deltaTime;
            if (m_shakeTime > m_shakeLerp)
            {
                m_lastShakePositionOffset = m_shakePositionOffset;

                m_shakeTime = 0.0f;
                m_shakePositionOffset = MathsHelper.RandomWithNegativeVector2() * m_shakeDistance;
            }

            actualPosition += Vector3.Lerp(m_lastShakePositionOffset, m_shakePositionOffset, m_shakeTime / m_shakeLerp);
        }


        transform.position = actualPosition;
    }

    Vector3 GetDesiredCameraPosition()
    {
        Vector3 cameraPosition = transform.position;
        if (m_triggerArea != null)
        {
            cameraPosition = m_triggerArea.transform.position;
        }
        else if(m_player != null)
        {
            cameraPosition = m_player.transform.position;
        }

        cameraPosition = new Vector3(cameraPosition.x, cameraPosition.y, m_cameraHeight);

        return cameraPosition;
    }


    public void RegisterTriggerArea(PlayerCameraTriggerArea _triggerArea)
    {
        m_triggerArea = _triggerArea;
        m_lerpPosition = transform.position;
        m_lerpAmount = 0.0f;
    }

    public void ClearTriggerArea()
    {
        m_triggerArea = null;
        m_lerpPosition = transform.position;
        m_lerpAmount = 0.0f;
    }


    public void SetCameraShake(bool _shake)
    {
        if(m_shaking != _shake)
        {
            m_shaking = _shake;
            m_shakeTime = 0.0f;
        }
    }
}
