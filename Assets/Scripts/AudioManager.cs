using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource swordSource;
    [SerializeField] AudioSource woodSource;
    [SerializeField] AudioSource bgm;
    [SerializeField] AudioClip equipSword;
    [SerializeField] AudioClip[] swordSwings;
    [SerializeField] AudioClip[] woodBreak;
    [SerializeField] AudioClip[] bgms;

    private void Start()
    {
        PlayBgm();
    }

    public void EquipSword()
    {
        swordSource.clip = equipSword;
        swordSource.Play();
    }

    public void SwingSword()
    {
        swordSource.clip = swordSwings[Random.Range(0, swordSwings.Length)];
        swordSource.Play();
    }

    public void BreakWood(int score)
    {
        if (score >= 90)
        {
            woodSource.clip = woodBreak[0];
            woodSource.Play();
        }
        else
        {

            woodSource.clip = woodBreak[1];
            woodSource.Play();
        }
    }
    public void PlayBgm()
    {
        bgm.clip = bgms[Random.Range(0, bgms.Length)];
        bgm.Play();
    }
}
