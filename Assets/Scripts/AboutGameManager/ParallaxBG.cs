using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBG : MonoBehaviour
{
    public float scrollSpeed = 2f;    // ความเร็วที่พื้นเลื่อน
    public float resetPositionX = -20f; // จุดที่พื้นรีเซ็ตกลับไปเริ่ม
    public float startPositionX = 20f; // จุดเริ่มต้นของพื้น

    private void Start()
    {

    }

    void Update()
    {
        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

        if (transform.position.x <= resetPositionX)
        {
            Vector3 newPos = transform.position;
            newPos.x = startPositionX;
            transform.position = newPos;
        }
    }
}
