using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankingContentScript : MonoBehaviour
{
    [SerializeField] TMP_Text rank;
    [SerializeField] TMP_Text diff;
    [SerializeField] TMP_Text score;
    [SerializeField] TMP_Text combo;
    [SerializeField] TMP_Text snm;

    public void SetContents(int grade, Ranking data)
    {
        rank.text = grade.ToString();

        switch (grade)
        {
            case 1:
                rank.color = Color.yellow;
                break;
            case 2:
                rank.color = Color.grey;
                break;
            case 3:
                rank.color = Color.green;
                break;
        }
        switch (data.difficulty)
        {
            case 1:
                diff.text = "Ezy";
                diff.color = Color.green;
                break;
            case 2:
                diff.text = "Normal";
                diff.color = Color.yellow;
                break;
            case 3:
                diff.text = "Perfect";
                diff.color = Color.red;
                break;
            default:
                diff.text = "?????";
                diff.color = Color.grey;
                break;
        }
        score.text = data.score.ToString();
        combo.text = data.combo.ToString();
        snm.text = data.slice + " | " + data.miss;
    }
}
