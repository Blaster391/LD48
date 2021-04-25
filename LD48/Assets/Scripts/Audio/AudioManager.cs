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
    private float m_bgmVolume = 1.0f;

    private bool m_changing = false;
    private bool m_source2Active = false;
    private float m_source2Lerp = 0.0f;

    void Awake()
    {
        GameMaster.RegisterAudioManager(this);
    }

    // Update is called once per frame
    void Update()
    {
        
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
        FadeToTrack(m_bgmTracks[_phase]);
    }

    public void GameOver()
    {
        StopBGM();
        PlayOnActiveBGMTrack(m_gameOverTrack, true);
    }

    public void StartBossBuildupMusic()
    {
        PlayOnActiveBGMTrack(m_bossBuildupMusic, true);
    }

    public void StartBossMusic()
    {
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
        PlayOnActiveBGMTrack(_clip);
    }
}
