using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip equipSword;
    [SerializeField] AudioClip[] swordSwings;
    [SerializeField] AudioClip[] woodBreak;
   
    public bool Playing()
    {
        return audioSource.isPlaying;
    }
    public void EquipSword()
    {
        audioSource.clip = equipSword;
        audioSource.Play();
    }

    public void SwingSword()
    {
        audioSource.clip = swordSwings[Random.Range(0, swordSwings.Length)];
        audioSource.Play();
    }

    public void BreakWood(int score)
    {
        if (score >= 90)
        {
            audioSource.clip = woodBreak[0];
            audioSource.Play();
        }
        else
        {

            audioSource.clip = woodBreak[1];
            audioSource.Play();
        }
    }
}
