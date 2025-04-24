using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public GameObject transitionPanel;
    private Animator transitionAnimator;

    // Start is called before the first frame update
    void Start()
    {
        if (transitionPanel == null)
            transitionPanel = GameObject.Find("TransitionPanel");

        transitionAnimator = transitionPanel.GetComponent<Animator>();
        
        StartCoroutine(DelayBeforeShop());
    }

    private IEnumerator DelayBeforeShop()
    {
        yield return new WaitForSecondsRealtime(5f); // รอ 5 วิแบบไม่หยุดเวลาเกม
        StartCoroutine(EnterShopRoutine());
    }

    private IEnumerator EnterShopRoutine()
    {
        transitionPanel.SetActive(true);
        transitionAnimator.SetTrigger("GoDown");  // ตั้ง Trigger ใน Animator

        //FindObjectOfType<BGMManager>().ChangeShopBGM(); // เปิด Sound

        yield return new WaitForSecondsRealtime(0.5f);
        Debug.Log("OpenButton");
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
