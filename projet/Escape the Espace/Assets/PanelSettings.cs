using UnityEngine;

public class PanelSettings : PanelScreen
{
    void Start()
    {
        
    }

    public override void UpdateScreen()
    {
    }

    public void SetVolume(float value)
    {

    }

    public void SetGraphics(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }
}
