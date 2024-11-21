using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class LaserPistol : MonoBehaviour
{
    public GameObject Laser;

    void Start()
    {
        UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        grabInteractable.activated.AddListener(OnActivated);
        grabInteractable.deactivated.AddListener(OnDeactivated);
    }

    private void OnActivated(ActivateEventArgs args)
    {
        Shoot();
    }

    private void OnDeactivated(DeactivateEventArgs args)
    {
        StopShooting();
    }

    public void Shoot()
    {
        Laser.SetActive(true);
    }

    public void StopShooting()
    {
        Laser.SetActive(false);
    }

    void Update()
    {

    }
}
