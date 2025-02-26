using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger Instance
    {
        get; private set;
    }

    string toLoad = "";
    [SerializeField] Image loadingImg;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }
    }

    //로드하고자하는 씬 이름 집어넣으면 로딩씬 -> 이름적은 씬으로 넘어감 / ""적으면 재시작
    public void ChangeSceneWithLoad(string sceneName)
    {
        if (Time.timeScale <= 0.1f)
        {
            Time.timeScale = 1;
        }
        Debug.Log(sceneName + " 로딩 진입");
        if (sceneName != "")
        {
            toLoad = sceneName;
        }
        SceneManager.LoadScene("LoadingScene"); //Async였다? 보장 못함. Load 가능할수도?
        StartCoroutine(Loading());
    }

    //로딩바 돌려주는 코루틴
    IEnumerator Loading()
    {
        yield return null;
        loadingImg = GameObject.Find("LoadingProgress").GetComponent<Image>();
        TMP_Text text = GameObject.Find("LoadingTitle").GetComponent<TMP_Text>();
        switch(GameManager.Instance.Difficulty)
        {
            case 1:
                text.text = "NewB Cutter";
                break;
            case 2:
                text.text = "Common Cutter";
                break;
            case 3:
                text.text = "perfect Cutter";
                break;
        }
        loadingImg.fillAmount = 1;
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(toLoad);
        asyncOp.allowSceneActivation = false;
        float timer = 0.0f;
        float fill = 0;
        while (!asyncOp.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
            if (asyncOp.progress < 0.9f)
            {
                fill = Mathf.Lerp(fill, asyncOp.progress, timer);
                loadingImg.fillAmount = 1 - fill;
                if (fill >= asyncOp.progress)
                {
                    timer = 0.0f;
                }
            }
            else
            {
                fill = Mathf.Lerp(fill, 1, timer);
                loadingImg.fillAmount = 1 - fill;
                if (fill >= 1)
                {
                    asyncOp.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
