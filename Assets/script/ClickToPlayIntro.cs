using UnityEngine;
using UnityEngine.Playables;

public class ClickToPlayIntro : MonoBehaviour
{
    public PlayableDirector director;

    private bool triggered = false;

    void Update()
    {
        if (triggered) return;

        bool clicked = Input.GetMouseButtonDown(0);
        bool touched = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;

        if (clicked || touched)
        {
            triggered = true;
            director.Play();
        }
    }
}
