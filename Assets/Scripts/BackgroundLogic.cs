using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackgroundLogic : MonoBehaviour
{
    void OnMouseDown()
    {
        if (!IsPointerOverUIObject())
        {
            TowerSelectorManager.shared.DeselectTower();
        }
    }
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);

        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        foreach (RaycastResult result in results)
        {
            if (result.gameObject.GetComponent<RectTransform>())
            {
                return true;
            }
        }
        return false;
    }
}
