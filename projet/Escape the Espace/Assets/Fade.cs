using System.Collections;
using UnityEngine;

public class Fade : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private float duration = 0.3f;

    void Start()
    {
        canvasGroup = this.GetComponent<CanvasGroup>();
    }


    public void FadeIn()
    {
        StartCoroutine(FadeInRoutine());
        StartCoroutine(FadeOutRoutine());

    }

    IEnumerator FadeInRoutine()
    {
        float time = Time.time;
        while (canvasGroup.alpha != 0)
        {
            canvasGroup.alpha = Mathf.SmoothStep(0, 1, (Time.time - time) / duration);
            yield return null;
        }
    }

    IEnumerator FadeOutRoutine()
    {
        float time = Time.time;
        while (canvasGroup.alpha != 0)
        {
            canvasGroup.alpha = Mathf.SmoothStep(1, 0, (Time.time - time) / duration);
            yield return null;
        }
    }
}
