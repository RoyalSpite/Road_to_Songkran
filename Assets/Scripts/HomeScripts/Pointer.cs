using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pointer : MonoBehaviour
{
    [SerializeField]private RectTransform pointer; 
    [SerializeField]private RectTransform audioBut; 
    [SerializeField]private RectTransform Logo; 
    [SerializeField]private float additionalOffsetX = -25f;

    private void Update()
    {
        GameObject hoveredObject = GetUIObjectUnderMouse();

        if (hoveredObject != null)
        {
            RectTransform buttonRect = hoveredObject.GetComponent<RectTransform>();
            if (buttonRect != null)
            {
                // อ่านขนาดของปุ่ม (width)
                float buttonWidth = buttonRect.rect.width;

                // คำนวณตำแหน่ง pointer (ไปทางซ้ายครึ่งหนึ่งของปุ่ม + offset เพิ่มเติม)
                Vector3 newPos = hoveredObject.transform.position;
                newPos.x -= (buttonWidth / 2f) + Mathf.Abs(additionalOffsetX);

                pointer.position = newPos;
            }
        }
        else
        {
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

        foreach (var result in raycastResults)
        {
            // เช็กว่าไม่ใช่ตัว pointer เอง
            if (result.gameObject != pointer.gameObject && result.gameObject != audioBut.gameObject && result.gameObject != Logo.gameObject)
            {
                return result.gameObject;
            }
        }

        return null;
    }
}
