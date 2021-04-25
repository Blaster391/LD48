using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AudioCharacter : MonoBehaviour
{
    protected AudioManager m_manager;
    protected GameObject m_currentAudioObject = null;
    protected int m_currentPriority = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_manager = GameMaster.GetAudioManager();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected bool CanPlayPriority(int _priority)
    {
        return m_currentAudioObject == null || m_currentPriority < _priority;
    }

    protected void PlayFromList(List<AudioClip> _clips, int _priority)
    {
        int index = Random.Range(0, _clips.Count);
        Play(_clips[index], _priority);
    }

    protected void Play(AudioClip _clip, int _priority)
    {
        if(CanPlayPriority(_priority))
        {
            m_currentPriority = _priority;
            m_currentAudioObject = m_manager.CreateAndPlayAudioObject(_clip);
        }
    }
}
