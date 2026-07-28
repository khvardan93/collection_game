using UnityEngine;

namespace GamePlay
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float pitch = 65f; // 60-70 degrees
        [SerializeField] private float distance = 12f; // distance from player
        [SerializeField] private float smoothTime = 0.15f;

        private Vector3 _velocity;

        private void LateUpdate()
        {
            if (!target) return;

            // Offset derived from pitch: back and up
            Quaternion rotation = Quaternion.Euler(pitch, 0f, 0f);
            Vector3 offset = rotation * new Vector3(0f, 0f, -distance);

            Vector3 desiredPos = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, desiredPos, ref _velocity, smoothTime);
            transform.rotation = rotation; // fixed angle, always looking down at 65°
        }
    }
}