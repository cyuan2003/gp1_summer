using UnityEngine;

public class TerritoryHover : MonoBehaviour
{
    public float hoverHeight = 0.3f;
    public float speed = 8f;

    private Vector3 basePos;
    private Vector3 targetPos;

    void Start()
    {
        basePos = transform.localPosition;
        targetPos = basePos;
    }

    void OnMouseEnter()
    {
        targetPos = basePos + Vector3.up * hoverHeight;
    }

    void OnMouseExit()
    {
        targetPos = basePos;
    }

    void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * speed);
    }
}
