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
        // pausePanel.SetActive(false);
        resumeButton.onClick.AddListener(ResumeGame);
    }
     
    public void PauseGame()
    {
        print("pause");
        // pausePanel.SetActive(true);
        pausePanel.gameObject.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        resumeButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);

        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        // pausePanel.SetActive(false);
        pausePanel.gameObject.GetComponent<Image>().color = new Color(1,1,1,0);

        resumeButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        Time.timeScale = 1f;
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
