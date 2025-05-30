using UnityEngine;
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
        GameManager.isGameOver = false;
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
        creditPanel.SetActive(true);
    }

    public void CreditClose()
    {
        creditPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
