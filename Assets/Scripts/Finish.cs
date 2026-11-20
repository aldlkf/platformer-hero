using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private AudioSource finishSound;

    private bool levelCompleted = false;

    private void Start()
    {
        finishSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем по компоненту, а не по имени объекта "Player"
        if (levelCompleted || !collision.TryGetComponent(out PlayerMovement _))
        {
            return;
        }

        levelCompleted = true;

        if (finishSound != null)
        {
            finishSound.Play();
        }

        Invoke(nameof(CompleteLevel), 2f);
    }

    private void CompleteLevel()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;

        // Если следующей сцены нет — возвращаемся в главное меню, а не получаем ошибку
        if (nextScene >= SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }

        SceneManager.LoadScene(nextScene);
    }
}