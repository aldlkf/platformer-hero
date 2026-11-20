using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    // Необязательно: Text на экране финала для показа итогового счёта
    [SerializeField] private Text resultText;

    private void Start()
    {
        if (resultText != null)
        {
            resultText.text = "Cherries collected: " + GameSession.TotalCherries;
        }
    }

    public void PlayAgain()
    {
        GameSession.Reset();
        SceneManager.LoadScene(1); // Level 1
    }

    public void MainMenu()
    {
        GameSession.Reset();
        SceneManager.LoadScene(0); // Start Screen
    }

    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}