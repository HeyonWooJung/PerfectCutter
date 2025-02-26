using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int difficulty;
    public int Difficulty
    {
        get
        {
            return difficulty;
        }
        set
        {
            if (1 > value && value > 3)
            {
                difficulty = 2;
            }
            else
            {
                difficulty = value;
            }
        }
    }

    public PerfectCutModeScript pcms;

    public event Action GameEnd;

    public List<Ranking> rankings;

    string filePath;

    public void SetPCMS(PerfectCutModeScript script) 
    {
        pcms = script;
    }

    public void PCGameStart()
    {
        pcms.StartGame();
    }

    public void GameOver()
    {
        if (GameEnd != null)
        {
            GameEnd();
        }
    }

    public void AddRanking(Ranking data)
    {
        rankings.Add(data);
        ShellSort(rankings);
        SaveData();
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(new Serialization<Ranking>(rankings), true); //뒤 인자값은 이쁘게 포맷할 때 씀 아이이뻐

        File.WriteAllText(filePath, json); //경로에 내용 저장
        Debug.Log("저장 완");
    }

    public void LoadData()
    {
        if (File.Exists(filePath) == true)
        {
            string json = File.ReadAllText(filePath); //파일에서 JSON 가져오기
            rankings = JsonUtility.FromJson<Serialization<Ranking>>(json).target;
            Debug.Log("로드 완");
        }
        else
        {
            //없네
            Debug.LogWarning("저장된 파일 없음");
        }
    }

    public static GameManager Instance
    {
        get; private set;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            filePath = Path.Combine(Application.persistentDataPath, "Rankings.json");
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }
    }

    void ShellSort(List<Ranking> rankList)
    {
        int n = rankList.Count;
        for (int gap = n / 2; gap > 0; gap /= 2) // 간격을 반씩 줄여가며 정렬
        {
            for (int i = gap; i < n; i++)
            {
                Ranking key = rankList[i];
                int j = i;

                while (j >= gap && rankList[j - gap].score < key.score)
                {
                    rankList[j] = rankList[j - gap];
                    j -= gap;
                }

                rankList[j] = key;
            }
        }
    }
}
