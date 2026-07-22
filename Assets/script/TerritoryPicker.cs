using UnityEngine;
using UnityEngine.EventSystems;

public class TerritoryPicker : MonoBehaviour
{
    public Camera cam;

    void Update()
    {
        bool clicked = Input.GetMouseButtonDown(0);
        bool touched = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
        if (!clicked && !touched) return;

        Vector3 screenPos = touched ? (Vector3)Input.GetTouch(0).position : Input.mousePosition;
        Ray ray = cam.ScreenPointToRay(screenPos);

        if (Physics.Raycast(ray, out RaycastHit hit)&&!EventSystem.current.IsPointerOverGameObject())
        {
            TerritoryView view = hit.collider.GetComponent<TerritoryView>();
            if (view != null)
                view.OnPicked();
        }
    }
}
