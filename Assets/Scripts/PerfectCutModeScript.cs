using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PerfectCutModeScript : MonoBehaviour
{
    [SerializeField] Slicer slicer;

    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text rightText;

    [SerializeField] ResultScript resultScript;
    int timer = 120;
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

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.SetPCMS(this);
        SetDifficulty(GameManager.Instance.Difficulty);
    }

    public void SetDifficulty(int diff)
    {
        switch(diff)
        {
            case 1:
                slicer.PrecisionSetting(20f, 20f);
                timer = 30;
                break;
            case 2:
                slicer.PrecisionSetting(25f, 10f);
                timer = 60;
                break;
            case 3:
                slicer.PrecisionSetting(25f, 5f);
                timer = 120;
                break;
        }
        timerText.text = "Time: " + timer;
    }

    public void StartGame()
    {
        rightText.text = "나무 판자의 화살표 각도에 맞춰\n" +
            "칼을 휘두르면 판자가 베어집니다 \n" +
            "정확한 각도로 벨 수록 점수가 높고 \n" +
            "각도가 틀어질수록 많이 깎입니다";
        StartCoroutine(Timer());
    }


    IEnumerator Timer()
    {
        while (timer >= 0)
        {
            yield return new WaitForSeconds(1f);
            timer--;
            timerText.text = "Time: " + timer;
        }
        GameManager.Instance.GameOver();
    }
}
