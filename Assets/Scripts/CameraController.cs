using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    // „ем меньше значение, тем жЄстче камера следует за игроком
    [SerializeField] private float smoothTime = 0.15f;

    private Vector3 velocity;

    // LateUpdate: камера двигаетс€ после того, как игрок уже сдвинулс€ Ч нет дрожани€
    private void LateUpdate()
    {
        if (player == null)
        {
            return;
        }

        Vector3 target = new Vector3(player.position.x, player.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime);
    }
}