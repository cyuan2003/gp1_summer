using UnityEngine;
using TMPro;

public class TurnDisplay : MonoBehaviour
{
    public GameManager game;
    public TMP_Text label;

    void Update()
    {
        label.text = "Turn " + game.currentTurn;
    }
}