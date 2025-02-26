using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Profiling;

[System.Serializable]
public class Ranking
{
    public int difficulty;
    public int score;
    public int combo;
    public int slice;
    public int miss;

    public Ranking(int difficulty, int score, int combo, int slice, int miss)
    {
        this.difficulty = difficulty;
        this.score = score;
        this.combo = combo;
        this.slice = slice;
        this.miss = miss;
    }
}

[Serializable]
public class Serialization<T>
{
    public Serialization(List<T> _target) => target = _target;
    public List<T> target;
}

public class RankingScript : MonoBehaviour
{
    [SerializeField] Transform rankParent;
    [SerializeField] RankingContentScript rankingContent;

    private void OnEnable()
    {
        ShowRankings();
    }

    public void ShowRankings()
    {
        for (int i = 0; i < rankParent.childCount; i++)
        {
            Destroy(rankParent.GetChild(i).gameObject);
        }
        GameManager.Instance.LoadData();
        int rank = 1;
        foreach(var content in GameManager.Instance.rankings)
        {
            RankingContentScript tempRCS = Instantiate(rankingContent.gameObject).GetComponent<RankingContentScript>();
            tempRCS.transform.SetParent(rankParent);
            tempRCS.SetContents(rank, content);
            rank++;
        }
    }


}
