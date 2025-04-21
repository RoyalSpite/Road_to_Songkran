using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [Header("Shop UI")]
    public GameObject shopUI; // หน้าร้านค้า
    public Button[] itemButtons; // ปุ่มสำหรับไอเทม 3 ชิ้น
    public Button continueButton; // ปุ่ม Continue

    [Header("Player")]
    public Player player; // อ้างอิง Player

    private void Start()
    {
        //shopUI.SetActive(false);

        // ผูกปุ่ม
        foreach (Button btn in itemButtons)
        {
            btn.onClick.AddListener(() => PurchaseItem(btn));
        }

        continueButton.onClick.AddListener(CloseShop);
    }

    private void PurchaseItem(Button clickedButton)
    {
        int itemIndex = System.Array.IndexOf(itemButtons, clickedButton);

        Debug.Log($"ซื้อไอเทมชิ้นที่ {itemIndex + 1}");

        // เลือก effect ตามไอเทม
        switch (itemIndex)
        {
            case 0:
                player.health = Mathf.Min(player.fullHealth, player.health + 30);
                break;
            case 1:
                player.carSpeedMultiplier += 0.5f;
                player.speedCountDown = 10f; // Speed up 10 วิ
                break;
            case 2:
                player.scoreMultiplier += 1;
                player.scoreCountDown = 15f; // Score up 15 วิ
                break;
        }

        // กดซื้อแล้ว disable ปุ่ม
        clickedButton.interactable = false;
    }

    public void OpenShop()
    {
        shopUI.SetActive(true);
        Time.timeScale = 0f; // หยุดเกม

        // เปิดปุ่มทั้งหมดอีกครั้ง
        foreach (Button btn in itemButtons)
        {
            btn.interactable = true;
        }
    }

    public void CloseShop()
    {
        shopUI.SetActive(false);
        Time.timeScale = 1f; // เล่นเกมต่อ
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Open Shop");
        }
    }
}
