using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenInput : MonoBehaviour
{
    public string nextScene = "MapMain";

    void Update()
    {
        bool clicked = Input.GetMouseButtonDown(0);
        bool touched = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;

        if (clicked || touched)
            SceneManager.LoadScene(nextScene);
    }
}
