using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorManager : MonoBehaviour
{
    public Text scoreText;
    public Text flyingScore;
    public Slicer slicer;
    int score = 0;

    Coroutine colorChange;
    // Start is called before the first frame update
    void Start()
    {
        slicer.OnScoreChanged += ChangeScore;
    }

    private void OnDestroy()
    {
        slicer.OnScoreChanged -= ChangeScore;
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
        tempFlying.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        tempFlying.text = value.ToString();

        if (value >= 0)
        {
            tempFlying.color = Color.green;
            scoreText.color = Color.green;
            colorChange = StartCoroutine(ChangeScoreColor(true));
        }
        else
        {
            tempFlying.color = Color.red;
            scoreText.color = Color.red;
            colorChange = StartCoroutine(ChangeScoreColor(false));
        }

        tempFlying.GetComponent<FlyingTextScript>().StartFly();
        score += value;
        if (score <= 0)
        {
            score = 0;
        }
        scoreText.text = "Score: " + score;
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
