using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ResultScript : MonoBehaviour
{
    [SerializeField] TMP_Text scoreTitle;
    [SerializeField] TMP_Text comboTitle;
    [SerializeField] TMP_Text hnmTitle;
    [SerializeField] TMP_Text diffText;

    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text comboText;
    [SerializeField] TMP_Text hitnMissText;

    [SerializeField] GameObject buttons;
    [SerializeField] Button retry;
    [SerializeField] Button mainMenu;

    private void Start()
    {
        switch(GameManager.Instance.Difficulty)
        {
            case 1:
                diffText.text = "Ezy";
                diffText.color = Color.green;
                break;
            case 2:
                diffText.text = "Normal";
                diffText.color = Color.yellow;
                break;
            case 3:
                diffText.text = "Perfect";
                diffText.color = Color.red;
                break;
        }
        retry.onClick.AddListener(() => SceneChanger.Instance.ChangeSceneWithLoad(""));
        mainMenu.onClick.AddListener(() => SceneChanger.Instance.ChangeSceneWithLoad("TitleScene"));
    }
    public void SetResult(int score, int maxCombo, int slice, int miss)
    {
        GameManager.Instance.AddRanking(new Ranking(GameManager.Instance.Difficulty, score, maxCombo, slice, miss));
        StartCoroutine(ShowResult(score, maxCombo, slice, miss));
    }

    IEnumerator ShowResult(int score, int maxCombo, int slice, int miss)
    {
        scoreTitle.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        yield return ShowScore(score);
        comboTitle.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        yield return ShowCombo(maxCombo);
        hnmTitle.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        yield return ShowSnM(slice, miss);
        buttons.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    IEnumerator ShowScore(int score)
    {
        scoreText.gameObject.SetActive(true);
        int tempScore = 0;
        int addScore = score / 75;
        if (addScore <= 0)
        {
            addScore = 1;
        }

        while (tempScore < score)
        {
            tempScore += addScore;
            if (tempScore >= score)
            {
                tempScore = score;
            }
            scoreText.text = tempScore.ToString();
            yield return new WaitForSeconds(0.02f);
        }
    }

    IEnumerator ShowCombo(int maxCombo)
    {
        comboText.gameObject.SetActive(true);
        int tempCombo = 0;
        int addCombo = maxCombo / 75;
        if (addCombo <= 0)
        {
            addCombo = 1;
        }

        while (tempCombo < maxCombo)
        {
            tempCombo += addCombo;
            if (tempCombo >= maxCombo)
            {
                tempCombo = maxCombo;
            }
            comboText.text = tempCombo.ToString();
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator ShowSnM(int slice, int miss)
    {
        hitnMissText.gameObject.SetActive(true);
        hitnMissText.text = "0 | 0";
        yield return new WaitForSeconds(0.5f);
        int tempSlice = 0;
        int addSlice = slice / 75;
        if (addSlice <= 0)
        {
            addSlice = 1;
        }

        while (tempSlice < slice)
        {
            tempSlice += addSlice;
            if (tempSlice >= slice)
            {
                tempSlice = slice;
            }
            hitnMissText.text = tempSlice + " | 0";
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(0.5f);

        int tempMiss = 0;
        int addMiss = miss / 75;
        if (addMiss <= 0)
        {
            addMiss = 1;
        }

        while (tempMiss < miss)
        {
            tempMiss += addMiss;
            if (tempMiss >= miss)
            {
                tempMiss = miss;
            }
            hitnMissText.text = slice + " | " + tempMiss;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.5f);
    }
}
