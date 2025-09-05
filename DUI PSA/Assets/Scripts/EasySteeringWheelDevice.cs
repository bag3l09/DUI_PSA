using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Controls;

[InputControlLayout(displayName = "Easy Steering Wheel")]
public class EasySteeringWheelDevice : InputDevice
{
    [InputControl(layout = "Axis", usage = "Steering")]
    public AxisControl steer { get; private set; }

    [InputControl(layout = "Axis", usage = "Accelerator")]
    public AxisControl throttle { get; private set; }

    [InputControl(layout = "Axis", usage = "Brake")]
    public AxisControl brake { get; private set; }

    protected override void FinishSetup()
    {
        base.FinishSetup();
        steer = GetChildControl<AxisControl>("steer");
        throttle = GetChildControl<AxisControl>("throttle");
        brake = GetChildControl<AxisControl>("brake");
    }
}
