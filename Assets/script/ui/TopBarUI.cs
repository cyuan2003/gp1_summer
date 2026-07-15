using UnityEngine;
using TMPro;

public class TopBarUI : MonoBehaviour
{
    public ResourceManager resources;
    public GameManager game;

    public TMP_Text troopsText;
    public TMP_Text suppliesText;
    public TMP_Text moraleText;
    public TMP_Text spiritText;
    public TMP_Text turnText;

    void OnEnable()
    {
        resources.OnChanged += Refresh;
    }

    void OnDisable()
    {
        resources.OnChanged -= Refresh;
    }

    void Start()
    {
        Refresh();
    }

    void Refresh()
    {
        if (troopsText != null) troopsText.text = "Troops " + resources.Get(ResourceType.Troops);
        if (suppliesText != null) suppliesText.text = "Supplies " + resources.Get(ResourceType.Supplies);
        if (moraleText != null) moraleText.text = "Morale " + resources.Get(ResourceType.Morale);
        if (spiritText != null) spiritText.text = "Spirit " + resources.Get(ResourceType.Spirit);
        if (turnText != null) turnText.text = "Turn " + game.currentTurn + " / " + game.config.maxTurns;
    }
}