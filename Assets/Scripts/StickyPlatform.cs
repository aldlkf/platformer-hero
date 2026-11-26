using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверка по компоненту вместо имени объекта "Player"
        if (collision.TryGetComponent(out PlayerMovement _))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Отпускаем игрока, только если он «прилип» именно к этой платформе
        if (collision.TryGetComponent(out PlayerMovement _) && collision.transform.parent == transform)
        {
            collision.transform.SetParent(null);
        }
    }
}