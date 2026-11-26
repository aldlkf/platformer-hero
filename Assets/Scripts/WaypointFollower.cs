using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    // Тип поля и имя не менял, чтобы не потерять ссылки на waypoint-ы в сценах
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float speed = 2f;

    private int currentWaypointIndex = 0;
    private const float ReachDistance = 0.1f;

    private void Start()
    {
        // Раньше пустой массив вызывал ошибку IndexOutOfRange каждый кадр
        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogWarning($"{name}: не заданы waypoints, движение отключено", this);
            enabled = false;
        }
    }

    private void Update()
    {
        Vector3 target = waypoints[currentWaypointIndex].transform.position;

        if (Vector2.Distance(target, transform.position) < ReachDistance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            target = waypoints[currentWaypointIndex].transform.position;
        }

        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
}