using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class MovableSkybox : MonoBehaviour
{
    [SerializeField] private XRJoystick controller;
    [Range(0,1)]
    [SerializeField] private float rotationSpeed;

    void Start()
    {
        rotationSpeed /= 4;
    }

    void Update()
    {
        if (controller.value != Vector2.zero)
        {
            Vector3 rotation = new Vector3(-controller.value.y * rotationSpeed, 0, controller.value.x * rotationSpeed);
            transform.Rotate(Quaternion.Inverse(Quaternion.Euler(transform.rotation.eulerAngles)) * rotation);
        }
    }
}
