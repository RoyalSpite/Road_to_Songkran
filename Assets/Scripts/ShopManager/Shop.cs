using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    [Header("Shop UI")]
    public GameObject shopUI; // หน้าร้านค้า
    public Button[] itemButtons; // ปุ่มสำหรับไอเทม 3 ชิ้น
    public Button continueButton; // ปุ่ม Continue
    private int point;
    [SerializeField] private TextMeshProUGUI playerPointText;
    private GameManager gameManager;

    [Header("Player")]
    public Player player; // อ้างอิง Player

    public AudioSource audioSorce;
    public GameObject shopTrigger;
    public ShopTrigger trigger;

    private void Start()
    {
        shopUI.SetActive(false);
        gameManager = FindObjectOfType<GameManager>();
        // ผูกปุ่ม
        foreach (Button btn in itemButtons)
        {
            btn.onClick.AddListener(() => PurchaseItem(btn));
        }
    }

    private void PurchaseItem(Button clickedButton)
    {
        int itemIndex = System.Array.IndexOf(itemButtons, clickedButton);

        Debug.Log($"ซื้อไอเทมชิ้นที่ {itemIndex + 1}");

        // เลือก effect ตามไอเทม
        switch (itemIndex)
        {
            case 1:
                player.health = player.fullHealth;
                break;
            case 2:
                player.baseCarSpeedMultiplier += 0.65f;
                player.carSpeedMultiplier += 0.65f;
                break;
            case 0:
                player.baseWeaponDamage += 1;
                player.weaponDamage += 1;
                break;
        }
        audioSorce.Play();
        // กดซื้อแล้ว disable ปุ่ม
        clickedButton.interactable = false;
    }

    public void OpenShop()
    {
        shopUI.SetActive(true);
        point = gameManager.Score;

        playerPointText.text = "Point : " + point;
        //Time.timeScale = 0f; // หยุดเกม

        // เปิดปุ่มทั้งหมดอีกครั้ง
        foreach (Button btn in itemButtons)
        {
            btn.interactable = true;
        }
    }

}
