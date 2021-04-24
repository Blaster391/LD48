using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> m_bgmTracks;

    [SerializeField]
    AudioClip m_endGameTrack;

    private AudioSource m_bgmSource;

    void Awake()
    {
        m_bgmSource = GetComponent<AudioSource>();

        GameMaster.RegisterAudioManager(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopBGM()
    {
        m_bgmSource.Stop();
    }

    public void PlayEndGameTrack()
    {
        m_bgmSource.PlayOneShot(m_endGameTrack);
    }
}
