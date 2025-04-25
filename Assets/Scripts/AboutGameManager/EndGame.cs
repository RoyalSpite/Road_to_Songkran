using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class EndGame : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject transitionPanel;
    [SerializeField] private GameObject buttonSet;
    [SerializeField] private TextMeshProUGUI playerPoint;
    [SerializeField] private TextMeshProUGUI playerDistance;

    public static int score;
    public static float distance;


    private Animator transitionAnimator;
    private Animator fadeAnimation;

    // Start is called before the first frame update
    void Start()
    {
        if (transitionPanel == null)
            transitionPanel = GameObject.Find("TransitionPanel");

        transitionAnimator = transitionPanel.GetComponent<Animator>();
        fadeAnimation = GetComponent<Animator>();   
        buttonSet.SetActive(false);
        
        StartCoroutine(DelayBeforeShop());
    }

    private IEnumerator DelayBeforeShop()
    {
        fadeAnimation.SetTrigger("FadeIn");
        yield return new WaitForSecondsRealtime(3f); // รอ 5 วิแบบไม่หยุดเวลาเกม
        StartCoroutine(EnterShopRoutine());
    }

    private IEnumerator EnterShopRoutine()
    {
        transitionPanel.SetActive(true);
        transitionAnimator.SetTrigger("GoDown");  // ตั้ง Trigger ใน Animator

        //FindObjectOfType<BGMManager>().ChangeShopBGM(); // เปิด Sound

        yield return new WaitForSecondsRealtime(2f);
        transitionPanel.SetActive(false);
        buttonSet.SetActive(true);
        WriteScore();
    }

    private void WriteScore()
    { 
        playerPoint.SetText("Your Score : " + score);
        playerDistance.SetText("Your Disatnce : " + Math.Round(distance, 2) + " km");
    }

    public void ToMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart(){
        GameManager.isGameOver = false;
        print(GameManager.isGameOver);
        GameManager.gameProgressModifier = 1f;
        SceneManager.LoadSceneAsync("PlayScene");
    }

}
