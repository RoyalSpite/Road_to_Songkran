using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private GameObject howToPanel;
    [SerializeField] private GameObject creditPanel;

    private void Start()
    {
        howToPanel.SetActive(false);
        creditPanel.SetActive(false);
    }

    public void PlayButtonTrigger()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void HowToPlayOpen()
    {
        howToPanel.SetActive(true);
    }

    public void HowToPlayClose()
    {
        howToPanel.SetActive(false);
    }

    public void CreditOpen()
    {
        howToPanel.SetActive(true);
    }

    public void CreditClose()
    {
        howToPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
