using UnityEngine;
using UnityEngine.Events;

public class EnergyTracker : MonoBehaviour
{
    private int count = 0;
    public UnityEvent EnergyAccumulated;

    public void CountEnergy(){
        count++;
    }
    // Update is called once per frame
    void Update()
    {
        if (count == 3){
            EnergyAccumulated.Invoke();
        }
    }
}
