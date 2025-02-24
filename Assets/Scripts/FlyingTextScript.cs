using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyingTextScript : MonoBehaviour
{
    [SerializeField] Text text;
    RectTransform rect;

    public void StartFly()
    {
        rect = text.GetComponent<RectTransform>();
        StartCoroutine(ChangeScoreColor());
    }

    IEnumerator ChangeScoreColor()
    {
        while (text.color.a > 0)
        {
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, rect.anchoredPosition.y + 2);
            if (text.color.a >= 0.03f)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - 0.025f);
            }
            else
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Destroy(gameObject);
    }
}
