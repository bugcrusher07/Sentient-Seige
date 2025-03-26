// using UnityEngine;

// public class CameraController : MonoBehaviour
// {
//     public Transform player;
//     public Vector3 offset = new Vector3(0, 2, -4); // Adjust for desired distance
//     public float sensitivity = 2f;

//     private float yaw = 0f;
//     private float pitch = 0f;

//     void Start()
//     {
//         Cursor.lockState = CursorLockMode.Locked; // Lock cursor for FPS-style camera
//     }

//     void LateUpdate()
//     {
//         // Mouse Input
//         yaw += Input.GetAxis("Mouse X") * sensitivity;
//         pitch -= Input.GetAxis("Mouse Y") * sensitivity;
//         pitch = Mathf.Clamp(pitch, -30f, 60f); // Prevent extreme up/down rotation

//         // Rotate camera around player
//         Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
//         transform.position = player.position + rotation * offset;
//         transform.LookAt(player.position);
//     }
// }
