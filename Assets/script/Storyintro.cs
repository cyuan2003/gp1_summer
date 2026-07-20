using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class StoryIntro : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI storyText;
    public GameObject storyCanvas;   // 拖入Canvas（或story父物体）
    public PlayableDirector cameraIntro;
    [Header("Time")]
    public float startDelay = 1f;
    public float showTime = 2f;
    public float fadeTime = 0.5f;
    public float endDelay = 2f;

    private bool hasPlayed = false;

    string[] story =
    {
        "The war has reached a stalemate,\nand enemy forces hold key positions.",

        "Our troops are short on supplies,\nbut the counterattack must begin.",

        "Headquarters has ordered\na full offensive to reclaim lost territory.",

        "You are the frontline commander.\nLead your troops to victory."
    };

    void Start()
    {
        if (!hasPlayed)
        {
            hasPlayed = true;
            StartCoroutine(PlayStory());
        }
    }

    IEnumerator PlayStory()
    {
        storyText.text = "";

        // 开始前等待
        yield return new WaitForSeconds(startDelay);

        foreach (string line in story)
        {
            storyText.text = line;

            Color c = storyText.color;
            c.a = 0;
            storyText.color = c;

            // 淡入
            float t = 0;
            while (t < fadeTime)
            {
                t += Time.deltaTime;
                c.a = Mathf.Lerp(0, 1, t / fadeTime);
                storyText.color = c;
                yield return null;
            }

            // 显示
            yield return new WaitForSeconds(showTime);

            // 淡出
            t = 0;
            while (t < fadeTime)
            {
                t += Time.deltaTime;
                c.a = Mathf.Lerp(1, 0, t / fadeTime);
                storyText.color = c;
                yield return null;
            }

            yield return new WaitForSeconds(0.3f);
        }

        // 最后等待
        yield return new WaitForSeconds(endDelay);

        // 隐藏整个Canvas（或story父物体）
        if (storyCanvas != null)
        {
            storyCanvas.SetActive(false);
            cameraIntro.Play();
        }
    }
}