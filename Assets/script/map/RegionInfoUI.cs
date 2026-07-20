using TMPro;
using UnityEngine;

public class RegionInfoUI : MonoBehaviour
{
    public static RegionInfoUI Instance;

    public GameObject panel;

    public TMP_Text titleText;
    public TMP_Text descriptionText;

    private void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void Open(string title, string content)
    {
        panel.SetActive(true);

        titleText.text = title;
        descriptionText.text = content;
    }

    public void Close()
    {
        panel.SetActive(false);
    }
}