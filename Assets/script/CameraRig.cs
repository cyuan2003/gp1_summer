using System.Collections;
using UnityEngine;

public class CameraRig : MonoBehaviour
{
    public Transform deskPose;
    public Transform mapPose;
    public float duration = 1.2f;

    void Start()
    {
        transform.position = deskPose.position;
        transform.rotation = deskPose.rotation;
    }

    public void MoveToMap()
    {
        StartCoroutine(Move(deskPose, mapPose));
    }

    public void MoveToDesk()
    {
        StartCoroutine(Move(mapPose, deskPose));
    }

    IEnumerator Move(Transform from, Transform to)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float k = Mathf.SmoothStep(0f, 1f, t / duration);
            transform.position = Vector3.Lerp(from.position, to.position, k);
            transform.rotation = Quaternion.Slerp(from.rotation, to.rotation, k);
            yield return null;
        }
        transform.position = to.position;
        transform.rotation = to.rotation;
    }
}
