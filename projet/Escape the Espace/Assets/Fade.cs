using System.Collections;
using UnityEngine;

public class Fade : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private float duration = 0.25f;

    void Start()
    {
        canvasGroup = this.GetComponent<CanvasGroup>();
        Debug.Log("this is our fokin canvas gruop :" + canvasGroup.name);
    }


    public void FadeIn()
    {
        StartCoroutine(FadeInRoutine());
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutRoutine());
    }

    IEnumerator FadeInRoutine()
    {
        float time = Time.time;
        while (canvasGroup.alpha != 1)
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
