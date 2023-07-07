using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Scrollbar bgmScrollbar;
    public Scrollbar sfxScrollbar;

    public AudioClip bgmClip;
    public float bgmVolume;
    AudioSource bgmSource;

    public AudioClip[] sfxClip;
    public float sfxVolume;
    public int channels;
    AudioSource[] sfxSource;
    int channelIndex;

    public enum SFX {Fire, Hit, Die, Swing,SoldierDie,WizardDid, Victory,MonsterDie}
    private void Awake()
    {
        instance = this;
        Init();
    }

    public void Init()
    {
        GameObject bgmObj = new GameObject("BgmPlayer");
        bgmObj.transform.parent = transform;
        bgmSource= bgmObj.AddComponent<AudioSource>();
        bgmSource.playOnAwake = false;
        bgmSource.loop = true;
        bgmSource.volume = bgmVolume;
        bgmSource.clip = bgmClip;

        GameObject sfxObj = new GameObject("sfxPlayer");
        sfxObj.transform.parent = transform;
        sfxSource=new AudioSource[channels];

        for(int i = 0; i < sfxSource.Length; i++)
        {
            sfxSource[i] = sfxObj.AddComponent<AudioSource>();
            sfxSource[i].playOnAwake = false;
            sfxSource[i].volume = sfxVolume;
        }
    }

    public void PlayerBGM(bool isPlay)
    {
        if(isPlay)
        {
            bgmSource.Play();
        }
        else
        {
            bgmSource.Stop();
        }
    }

    public void PlaySFX(SFX sfx)
    {
        for(int i=0;i<sfxSource.Length;i++)
        {
            int loopIndex = (i + channelIndex)%sfxSource.Length;

            if (sfxSource[loopIndex].isPlaying)
                continue;

            channelIndex = loopIndex;
            sfxSource[loopIndex].clip = sfxClip[(int)sfx];
            sfxSource[loopIndex].Play();
            break;
        }
        
    }
    public void BGMSoundValue()
    {
        bgmSource.volume = bgmScrollbar.value;
    }
    public void SFMSoundValue()
    {
        for (int i = 0; i < sfxSource.Length; i++)
        {
            sfxSource[i].volume = sfxScrollbar.value;
        }
    }
}
