using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using EasySteering;

public class EasySteeringInputSystemDriver : MonoBehaviour
{
    private EasySteeringWheelDevice device;

    void OnEnable()
    {
        InputSystem.RegisterLayout<EasySteeringWheelDevice>(
            matches: new InputDeviceMatcher()
                .WithInterface("EasySteeringWheel"));

        device = InputSystem.AddDevice<EasySteeringWheelDevice>("EasySteeringWheel");
    }

    void Update()
    {
        if (device == null) return;

        WheelInputManager.Tick(); // Refresh wheel input

        InputSystem.QueueDeltaStateEvent(device.steer, WheelInputManager.steer);
        InputSystem.QueueDeltaStateEvent(device.throttle, WheelInputManager.throtle); // note spelling
        InputSystem.QueueDeltaStateEvent(device.brake, WheelInputManager.brake);
    }

    void OnDisable()
    {
        if (device != null)
        {
            InputSystem.RemoveDevice(device);
            device = null;
        }
    }
}
