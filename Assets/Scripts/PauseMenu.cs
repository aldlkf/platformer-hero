using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private string menuScene = "Start Screen";

    private bool isPaused;

    private void Start()
    {
        pausePanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Toggle();
        }
    }

    // Привязывается и к кнопке Resume
    public void Toggle()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
        AudioListener.pause = isPaused;
    }

    public void BackToMenu()
    {
        ResetTimeAndAudio();
        SceneManager.LoadScene(menuScene);
    }

    private void OnDestroy()
    {
        ResetTimeAndAudio();
    }

    private void ResetTimeAndAudio()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }
}