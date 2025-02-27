using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    RectTransform rect;
    [SerializeField] Button resume;
    [SerializeField] Button retry;
    [SerializeField] Button title;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.pauseScript = this;
        rect = GetComponent<RectTransform>();
        resume.onClick.AddListener(() =>
        {
            Cursor.lockState = CursorLockMode.Locked;
            gameObject.SetActive(false);
            Time.timeScale = 1f;

        }); 
        retry.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            SceneChanger.Instance.ChangeSceneWithLoad("");

        });
        title.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            SceneChanger.Instance.ChangeSceneWithLoad("TitleScene");

        });
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameManager.Instance.pauseScript = null;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        StartCoroutine(ShowPopUp());
    }

    IEnumerator ShowPopUp()
    {
        Debug.Log("진입");
        rect.anchoredPosition = new Vector2(0, 890);
        while(rect.anchoredPosition.y > 0)
        {
            rect.anchoredPosition = new Vector2(0, rect.anchoredPosition.y - 20);
            if (rect.anchoredPosition.y <= 0)
            {
                rect.anchoredPosition = new Vector2(0, 0);
            }
            yield return new WaitForSecondsRealtime(0.001f);
        }
    }
}
