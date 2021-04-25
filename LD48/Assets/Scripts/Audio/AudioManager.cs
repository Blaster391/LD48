using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_audioObject;

    [SerializeField]
    private List<AudioClip> m_bgmTracks;

    [SerializeField]
    private List<AudioClip> m_ghostAudio;

    [SerializeField]
    AudioClip m_bossBuildupMusic;

    [SerializeField]
    AudioClip m_bossMusic;
    [SerializeField]
    AudioClip m_bossMusicFinalPhase;
    [SerializeField]
    AudioClip m_bossKilled;

    [SerializeField]
    AudioClip m_endGameTrack;

    [SerializeField]
    AudioClip m_gameOverTrack;

    [SerializeField]
    private AudioSource m_bgmSource1;
    [SerializeField]
    private AudioSource m_bgmSource2;

    [SerializeField]
    private float m_fadeTime = 3.0f;

    [SerializeField]
    private float m_bossFadeOutTime = 3.0f;

    [SerializeField]
    private float m_bgmVolume = 1.0f;

    private bool m_changing = false;
    private bool m_source2Active = false;
    private float m_source2Lerp = 0.0f;
    private int m_currentBGMPhase = -1;
    private bool m_bossWarmupFade = false;
    private GameObject m_ghostSound = null;

    private float m_fadeOutLerp = 0.0f;

    void Awake()
    {
        GameMaster.RegisterAudioManager(this);
        StartBGMPhase(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(m_bossWarmupFade)
        {
            float changeRate = 1 / m_bossFadeOutTime;
            float changeDelta = changeRate * Time.deltaTime;
            m_fadeOutLerp += changeDelta;
            m_fadeOutLerp = Mathf.Clamp01(m_fadeOutLerp);

            m_bgmSource1.volume = (1.0f - m_fadeOutLerp) * m_bgmVolume;
            m_bgmSource2.volume = (1.0f - m_fadeOutLerp) * m_bgmVolume;

            if (m_fadeOutLerp >= 1.0f)
            {
                PlayOnActiveBGMTrack(m_bossBuildupMusic, true);

                m_bgmSource1.volume = m_bgmVolume;
                m_bgmSource2.volume = m_bgmVolume;

                m_bossWarmupFade = false;
            }
        }
        else if(m_changing)
        {
            if(m_source2Active && m_source2Lerp >= 1.0f)
            {
                m_bgmSource1.Stop();
                m_changing = false;
            }
            else if (!m_source2Active && m_source2Lerp <= 0.0f)
            {
                m_bgmSource2.Stop();
                m_changing = false;
            }

            float changeRate = 1 / m_fadeTime;
            float changeDelta = changeRate * Time.deltaTime;

            m_source2Lerp += (m_source2Active ? changeDelta : -changeDelta);
            m_source2Lerp = Mathf.Clamp01(m_source2Lerp);

            m_bgmSource1.volume = (1.0f - m_source2Lerp) * m_bgmVolume;
            m_bgmSource2.volume = (m_source2Lerp) * m_bgmVolume;
        }
    }

    public void TriggerGhostSound()
    {
        if(m_ghostSound == null)
        {
            m_ghostSound = CreateAndPlayAudioObject(m_ghostAudio[Random.Range(0, m_ghostAudio.Count)]);
            m_ghostSound.GetComponent<AudioSource>().panStereo = (Random.value > 0.5) ? -1.0f : 1.0f;
            m_ghostSound.GetComponent<AudioSource>().volume = 0.5f;
        }
    }

    public GameObject CreateAndPlayAudioObject(AudioClip _clip)
    {
        GameObject audioObject = Instantiate<GameObject>(m_audioObject, transform.position, Quaternion.identity, transform);

        audioObject.GetComponent<AudioObject>().Initialize(_clip);

        return audioObject;
    }

    public void StopBGM()
    {
        m_bgmSource1.Stop();
        m_bgmSource2.Stop();

        m_source2Lerp = m_source2Active ? 1.0f : 0.0f;
    }

    public void StartBGMPhase(int _phase)
    {
        if(_phase != m_currentBGMPhase)
        {
            m_currentBGMPhase = _phase;
            FadeToTrack(m_bgmTracks[_phase]);
        }

    }

    public void GameOver()
    {
        StopBGM();
        PlayOnActiveBGMTrack(m_gameOverTrack, true);
    }

    public void StartBossBuildupMusic()
    {
        m_bossWarmupFade = true;
    }

    public void StartBossMusic()
    {
        StopBGM();

        m_bossWarmupFade = false;
        m_source2Active = !m_source2Active;
        m_changing = true;

        PlayOnActiveBGMTrack(m_bossMusic);
    }

    public void StartBossLastPhase()
    {
        FadeToTrack(m_bossMusicFinalPhase);
    }

    public void PlayBossKilled()
    {
        PlayOnActiveBGMTrack(m_bossKilled, true);
    }

    public void PlayEndGameTrack()
    {
        PlayOnActiveBGMTrack(m_endGameTrack, true);
    }

    private void PlayOnActiveBGMTrack(AudioClip _clip, bool _oneShot = false)
    {
        AudioSource source = m_bgmSource1;
        if(m_source2Active)
        {
            source = m_bgmSource2;
        }
  
        if(_oneShot)
        {
            StopBGM();
            source.PlayOneShot(_clip);
        }
        else
        {
            source.clip = _clip;
            source.Play();
        }
    }

    private void FadeToTrack(AudioClip _clip)
    {
        m_source2Active = !m_source2Active;
        m_changing = true;
        PlayOnActiveBGMTrack(_clip);
        if(m_source2Active)
        {
            m_bgmSource2.time = m_bgmSource1.time;
        }
        else
        {
            m_bgmSource1.time = m_bgmSource2.time;
        }

    }
}
