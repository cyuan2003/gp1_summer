using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TextSequence : MonoBehaviour
{
    public CanvasGroup group;
    public TMP_Text textLabel;
    [TextArea] public string[] lines;
    public bool beginOnEnable = true;
    public UnityEvent onFinished;

    private int index = 0;
    private bool running = false;

    void OnEnable()
    {
        if (beginOnEnable)
            Begin();
    }
    public void BeginWith(string[] customLines)
    {
        lines = customLines;
        Begin();
    }

    public void Begin()
    {
        if (lines.Length == 0) { Finish(); return; }
        index = 0;
        running = true;
        if (group != null) group.alpha = 1f;
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
        onFinished.Invoke();
        gameObject.SetActive(false);
    }
}
