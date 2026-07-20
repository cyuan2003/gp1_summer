using UnityEngine;

public class RegionButton : MonoBehaviour
{
    public string regionName;

    [TextArea(5, 10)]
    public string description;

    public void ClickRegion()
    {
        RegionInfoUI.Instance.Open(regionName, description);
    }
}