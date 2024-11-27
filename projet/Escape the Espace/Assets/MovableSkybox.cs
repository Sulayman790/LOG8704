using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class MovableSkybox : MonoBehaviour
{
    [SerializeField] private XRJoystick controller;
    [Range(0,1)]
    [SerializeField] private float rotationSpeed;

    private bool joystickActive;

    private void Start()
    {
        rotationSpeed /= 2;
    }

    private void FixedUpdate()
    {
        if (!joystickActive)
        {
            return;
        }
        if (controller.value != Vector2.zero)
        {
            Vector3 rotation = new Vector3(-controller.value.y * rotationSpeed, - controller.value.x * rotationSpeed, 0);
            transform.Rotate(Quaternion.Inverse(Quaternion.Euler(transform.rotation.eulerAngles)) * rotation);
        }
    }

    public void EnableJoystick()
    {
        joystickActive = true;
        Debug.Log("Enable Joystick");
    }
}
