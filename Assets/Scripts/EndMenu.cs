using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    // Необязательно: Text на экране финала для показа итогового счёта
    [SerializeField] private Text resultText;

    // Имена сцен вместо «магических» индексов 0 и 1 (сцены должны быть в Build Settings)
    [SerializeField] private string firstLevelScene = "Level 1";
    [SerializeField] private string menuScene = "Start Screen";

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
        SceneManager.LoadScene(firstLevelScene);
    }

    public void MainMenu()
    {
        GameSession.Reset();
        SceneManager.LoadScene(menuScene);
    }

    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}