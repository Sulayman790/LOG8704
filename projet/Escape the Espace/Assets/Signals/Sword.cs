using UnityEngine;
using UnityEngine.Events;

public class Sword : MonoBehaviour
{
    private int breakableCount = 0;
    public int totalBreakables = 3;
    public UnityEvent AsteroidsBroken;

    void OnEnable()
    {
        Breakable.OnBreakableDestroyed += HandleBreakableDestroyed;
    }

    void OnDisable()
    {
        Breakable.OnBreakableDestroyed -= HandleBreakableDestroyed;
    }

    private void HandleBreakableDestroyed()
    {
        breakableCount++;
        if (breakableCount >= totalBreakables)
        {
            TriggerEvent();
        }
    }

    private void TriggerEvent()
    {
        AsteroidsBroken.Invoke();
    }
}
