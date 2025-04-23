using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    public GameObject shopUI;
    public GameObject transitionPanel;
    public SpriteBlink blink;

    private Animator transitionAnimator;

    private bool isInShop = false;

    private void Start()
    {

        if (transitionPanel == null)
            transitionPanel = GameObject.Find("TransitionPanel");

        transitionAnimator = transitionPanel.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isInShop)
        {
            if (shopUI == null)
            {
                shopUI = GameObject.Find("ShopUI");
            }
            isInShop = true;
            StartCoroutine(EnterShopRoutine());
        }
    }

    private IEnumerator EnterShopRoutine()
    {
        transitionPanel.SetActive(true);
        transitionAnimator.SetTrigger("GoLeft");  // ตั้ง Trigger ใน Animator

        FindObjectOfType<BGMManager>().ChangeShopBGM();

        // รอจนกว่าจะบังเต็ม (เวลาตาม Animation ประมาณกลางๆ) เช่น 0.5 วิ
        yield return new WaitForSecondsRealtime(0.5f);

        Time.timeScale = 0f;
        shopUI.SetActive(true);
        blink.enabled = true;
    }

    public void ExitShop()
    {
        StartCoroutine(ExitShopRoutine());
    }

    private IEnumerator ExitShopRoutine()
    {
        // ย้อน Transition กลับ หรือจางหายไปก็ได้
        transitionAnimator.SetTrigger("GoRight"); // หรือ FadeOut ก็ได้

        yield return new WaitForSecondsRealtime(1f);

        Time.timeScale = 1f;
        //transitionPanel.SetActive(false);
        FindObjectOfType<BGMManager>().ChangeInGameBGM();
        shopUI.SetActive(false);
        isInShop = false;
    }
}
