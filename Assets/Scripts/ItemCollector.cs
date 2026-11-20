using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private Text cherriesText;

    [SerializeField] private AudioSource collectionSoundEffect;

    private void Start()
    {
        // Запоминаем счёт на старте уровня, чтобы при смерти откатиться к нему
        GameSession.BeginLevel();
        UpdateText();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            GameSession.AddCherry();
            UpdateText();
        }
    }

    private void UpdateText()
    {
        cherriesText.text = "Cherries: " + GameSession.TotalCherries;
    }
}