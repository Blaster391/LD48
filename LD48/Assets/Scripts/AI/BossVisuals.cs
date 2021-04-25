using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossVisuals : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer m_body;
    [SerializeField]
    private SpriteRenderer m_face;

    [SerializeField]
    private List<Sprite> m_bodySprites;
    [SerializeField]
    private List<Sprite> m_finalFormSprites;

    [SerializeField]
    private List<Sprite> m_restFaceSprites;
    [SerializeField]
    private List<Sprite> m_shotgunFaceSprites;
    [SerializeField]
    private List<Sprite> m_roarFaceSprites;
    [SerializeField]
    private List<Sprite> m_spinFaceSprites;
    [SerializeField]
    private List<Sprite> m_summonFaceSprites;
    [SerializeField]
    private List<Sprite> m_pouncingFaceSprites;

    [SerializeField]
    private Color m_stage3ColourBody;
    [SerializeField]
    private Color m_stage3ColourFace;

    [SerializeField]
    private float m_updateSpeed = 1.5f;

    [SerializeField]
    private float m_finalPhaseUpdateSpeed = 2.5f;

    private int m_index = 0;
    private float m_lastUpdateTime = 0.0f;
    private bool m_finalForm = false;

    private BossAI m_bossAI = null;

    void Start()
    {
        m_lastUpdateTime = Random.value;
        m_bossAI = GetComponent<BossAI>();

    }

    public void FinalForm()
    {
        m_finalForm = true;
        m_body.color = m_stage3ColourBody;
        m_face.color = m_stage3ColourFace;
    }

    // Update is called once per frame
    void Update()
    {
        bool isFinalForm = m_finalForm;

        m_lastUpdateTime += Time.deltaTime;
        float updateTime = 1 /(isFinalForm ? m_updateSpeed : m_finalPhaseUpdateSpeed);

        if (m_lastUpdateTime > updateTime)
        {
            m_lastUpdateTime = 0;
            m_index++;

            if (m_index > 100000000)
            {
                m_index = 0;
            }

            if (isFinalForm)
            {
                m_body.sprite = m_finalFormSprites[m_index % m_finalFormSprites.Count];
            }
            else
            {
                m_body.sprite = m_bodySprites[m_index % m_bodySprites.Count];
            }


            List<Sprite> faceSprites = m_restFaceSprites;
            switch (m_bossAI.GetState())
            {
                case BossState.Roar:
                    faceSprites = m_roarFaceSprites;
                    break;
                case BossState.Rest:
                    faceSprites = m_restFaceSprites;
                    break;
                case BossState.PounceCharging:
                    faceSprites = m_pouncingFaceSprites;
                    break;
                case BossState.Pouncing:
                    faceSprites = m_pouncingFaceSprites;
                    break;
                case BossState.SpinAttack:
                    faceSprites = m_spinFaceSprites;
                    break;
                case BossState.Shotgun:
                    faceSprites = m_shotgunFaceSprites;
                    break;
                case BossState.Summon:
                    faceSprites = m_summonFaceSprites;
                    break;
                case BossState.Dying:
                    faceSprites = m_roarFaceSprites;
                    break;
            }

            m_face.sprite = faceSprites[m_index % faceSprites.Count];
        }


    }
}
