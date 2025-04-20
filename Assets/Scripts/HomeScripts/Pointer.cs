using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pointer : MonoBehaviour
{
    public RectTransform pointer; // �ٻ�١�÷����ҡ����Ѻ

    private void Update()
    {
        // ���������������躹����
        GameObject hoveredObject = GetUIObjectUnderMouse();

        if (hoveredObject != null)
        {
            // ���� pointer 价����˹觻�������������
            pointer.position = hoveredObject.transform.position;
        }
        else
        {
            // ��������������� �Ҩ�Ы�͹ pointer ������
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
