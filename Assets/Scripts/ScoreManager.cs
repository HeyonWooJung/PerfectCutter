using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text comboText;
    public Text flyingScore;
    public Slicer slicer;
    [SerializeField] ResultScript resultScript;

    int score = 0;
    int combo = 0;
    int multi = 1;

    int maxCombo = 0;
    int sliceCount = 0;
    int missCount = 0;

    Coroutine colorChange;
    // Start is called before the first frame update
    void Start()
    {
        slicer.OnScoreChanged += ChangeScore;
        GameManager.Instance.GameEnd += SetScore;
    }

    private void OnDestroy()
    {
        slicer.OnScoreChanged -= ChangeScore;
        GameManager.Instance.GameEnd -= SetScore;
    }

    public void ChangeScore(int value)
    {
        if (colorChange != null)
        {
            StopCoroutine(colorChange);
            scoreText.color = Color.white;
        }
        Text tempFlying = Instantiate(flyingScore);
        tempFlying.transform.SetParent(transform);
        tempFlying.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 50);

        if (value >= 0)
        {
            tempFlying.color = Color.green;
            scoreText.color = Color.green;
            colorChange = StartCoroutine(ChangeScoreColor(true));
            combo++;
            if (combo > maxCombo)
            {
                maxCombo = combo;
            }
            sliceCount++;
        }
        else
        {
            tempFlying.color = Color.red;
            scoreText.color = Color.red;
            colorChange = StartCoroutine(ChangeScoreColor(false));
            combo = 0;
            multi = 1;
            missCount++;
        }

        if (combo > 0 && combo % 10 == 0 && multi < 5)
        {
            multi++;
        }

        value *= multi;
        tempFlying.text = value.ToString() + (value > 100 ? "!" : "");
        tempFlying.GetComponent<FlyingTextScript>().StartFly();
        score += value;
        if (score <= 0)
        {
            score = 0;
        }
        scoreText.text = "Score: " + score;
        if (combo == 0)
        {
            comboText.text = "";
        }
        else
        {
            comboText.text = combo + " Combo";
        }
        int comboTemp = combo / 10;
        for (int i = 0; i < comboTemp; i++)
        {
            comboText.text = comboText.text + '!';
        }

    }

    public void SetScore()
    {
        resultScript.gameObject.SetActive(true);
        resultScript.SetResult(score, maxCombo, sliceCount, missCount);
    }

    IEnumerator ChangeScoreColor(bool isPlus)
    {
        if (isPlus)
        {
            while (scoreText.color != Color.white)
            {
                if (scoreText.color.b <= 0.94)
                {
                    scoreText.color = new Color(scoreText.color.r + 0.025f, 1, scoreText.color.b + 0.025f);
                }
                else
                {
                    scoreText.color = Color.white;
                }
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
        else
        {
            while (scoreText.color != Color.white)
            {
                if (scoreText.color.b <= 0.94)
                {
                    scoreText.color = new Color(1, scoreText.color.g + 0.05f, scoreText.color.b + 0.025f);
                }
                else
                {
                    scoreText.color = Color.white;
                }
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
    }
}
