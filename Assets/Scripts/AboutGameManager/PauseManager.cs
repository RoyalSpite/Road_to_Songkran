using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button quitButton;

    void Start()
    {
        pausePanel.SetActive(false);
        resumeButton.onClick.AddListener(ResumeGame);
    }
     
    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
