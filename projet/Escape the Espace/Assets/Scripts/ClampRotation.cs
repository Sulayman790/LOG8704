using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class ClampRotation : MonoBehaviour
{
    [SerializeField] private Vector3 minRotation;
    [SerializeField] private Vector3 maxRotation;
    private IDisposable disposable;

    private void Awake()
    {
        InputSystem.onEvent += LimitRotation;
    }

    private void OnDisable()
    {
        InputSystem.onEvent -= LimitRotation;
    }

    void LimitRotation(InputEventPtr input, InputDevice device)
    {
        Vector3 currentAngle = transform.rotation.eulerAngles;
        if (currentAngle.x > 180)
        {
            currentAngle.x -= 360f;
        }
        if (currentAngle.y > 180)
        {
            currentAngle.y -= 360f;
        }
        if (currentAngle.z > 180)
        {
            currentAngle.z -= 360f;
        }
        Vector3 clamped_angle = new Vector3(
            Mathf.Clamp(currentAngle.x, minRotation.y, maxRotation.x),
            Mathf.Clamp(currentAngle.y, minRotation.y, maxRotation.y),
            Mathf.Clamp(currentAngle.z, minRotation.z, maxRotation.z)
        );
        Debug.Log(clamped_angle);

        transform.eulerAngles = clamped_angle;
    }
}
