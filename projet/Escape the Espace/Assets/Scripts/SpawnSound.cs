using UnityEngine;

public class PlaySoundOnActive : MonoBehaviour
{
    public AudioSource audioSource; // Drag and drop your AudioSource here in the Inspector.

    private void OnEnable()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}