using System;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TextSequence : MonoBehaviour
{
    public CanvasGroup group;
    public TMP_Text textLabel;
    [TextArea] public string[] lines;
    public UnityEvent onFinished;

    private int index = 0;
    private bool running = false;

    public void Begin()
    {
        if (lines.Length == 0) { Finish(); return; }
        index = 0;
        running = true;
        group.gameObject.SetActive(true);
        group.alpha = 1f;
        Show();
    }

    void Update()
    {
        if (!running) return;

        bool clicked = Input.GetMouseButtonDown(0);
        bool touched = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
        if (clicked || touched)
            Next();
    }

    void Next()
    {
        index++;
        if (index >= lines.Length)
            Finish();
        else
            Show();
    }

    void Show()
    {
        textLabel.text = lines[index];
    }

    void Finish()
    {
        running = false;
        group.gameObject.SetActive(false);
        onFinished.Invoke();
    }
}
