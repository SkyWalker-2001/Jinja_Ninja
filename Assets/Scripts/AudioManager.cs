using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] AudioSource[] sfx;
    [SerializeField] AudioSource[] bgm;

    private int bgmIndex;

    //  SingleTone
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        // if BGM is not Playing
        if (!bgm[bgmIndex].isPlaying)
        {
            bgm[bgmIndex].Play();
        }
    }

    public void PlaySFX(int sfxToPlay)
    {
        if (sfxToPlay < sfx.Length)
        {
            sfx[sfxToPlay].pitch = Random.Range(0.85f, 1.15f);
            sfx[sfxToPlay].Play();
        }
    }

    public void StopSFX(int sfxToStop)
    {
        sfx[sfxToStop].Stop();
    }

    public void PlayBGM(int bgmToPlay)
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            // First need to Stop all the BG Music
            bgm[i].Stop();
        }

        bgm[bgmToPlay].Play();
    }

    public void PlayBGM_Random()
    {
        bgmIndex = Random.Range(0, bgm.Length);

        PlayBGM(bgmIndex);
    }
}
