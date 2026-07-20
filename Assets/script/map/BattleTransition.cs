using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BattleTransition : MonoBehaviour
{
    public Image fadeImage;

    public GameObject mapPanel;

    public GameObject infoPanel;

    public GameObject cardPanel;

    public float fadeTime = 2f;

    public void BeginBattle()
    {
        StartCoroutine(FadeRoutine());
    }

    IEnumerator FadeRoutine()
    {
        infoPanel.SetActive(false);

        Color c = fadeImage.color;

        float t = 0;

        while (t < fadeTime)
        {
            t += Time.deltaTime;

            c.a = Mathf.Lerp(0, 1, t / fadeTime);

            fadeImage.color = c;

            yield return null;
        }

        mapPanel.SetActive(false);

        cardPanel.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        t = 0;

        while (t < fadeTime)
        {
            t += Time.deltaTime;

            c.a = Mathf.Lerp(1, 0, t / fadeTime);

            fadeImage.color = c;

            yield return null;
        }
    }
}