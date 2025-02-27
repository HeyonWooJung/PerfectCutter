using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitleSceneScript : MonoBehaviour
{
    public GameObject diffPanel;
    public GameObject rankPanel;
    public TMP_Text titleLeft;
    public AudioSource audioSource;
    public AudioClip btnMouseOver;
    public AudioClip btnClick;

    public void BtnMouseOver()
    {
        audioSource.clip = btnMouseOver;
        audioSource.Play();
    }

    public void BtnClick()
    {
        audioSource.clip = btnClick;
        audioSource.Play();
    }

    public void SetDiffPanel(bool state)
    {
        diffPanel.SetActive(state);
    }

    public void SetRankPanel(bool state)
    {
        rankPanel.SetActive(state);
    }

    public void ChangeDifficulty(string text)
    {
        titleLeft.text = text;
    }

    public void SelectDifficulty(int diff)
    {
        GameManager.Instance.Difficulty = diff;
        SceneChanger.Instance.ChangeSceneWithLoad("PerfectCutter");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
