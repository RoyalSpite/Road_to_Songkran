using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pointer : MonoBehaviour
{
    public RectTransform pointer; // รูปลูกศรที่อยากให้ขยับ

    private void Update()
    {
        // เช็กว่าเมาส์ชี้อยู่บนอะไร
        GameObject hoveredObject = GetUIObjectUnderMouse();

        if (hoveredObject != null)
        {
            // ย้าย pointer ไปที่ตำแหน่งปุ่มที่เมาส์ชี้
            pointer.position = hoveredObject.transform.position;
        }
        else
        {
            // ถ้าไม่ชี้อะไรเลย อาจจะซ่อน pointer ไว้ก็ได้
            // pointer.gameObject.SetActive(false);
        }
    }

    private GameObject GetUIObjectUnderMouse()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        var raycastResults = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, raycastResults);

        if (raycastResults.Count > 0)
        {
            return raycastResults[0].gameObject;
        }
        return null;
    }
}
