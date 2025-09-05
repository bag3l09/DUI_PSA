using UnityEngine;

namespace Ezereal
{
    public class EzerealCameraController : MonoBehaviour
    {
        [SerializeField] private GameObject camera; // Only one camera now

        private void Awake()
        {
            camera.SetActive(true); // Ensure the single camera is enabled
        }
    }
}

