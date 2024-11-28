using UnityEngine;

public class PanelSettings : PanelScreen
{
    private AudioSource yappingDude;
    void Start()
    {
        yappingDude = GameObject.Find("StorySpeaker").GetComponent<AudioSource>();
    }

    public override void UpdateScreen()
    {
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void SetGraphics(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }

    public void ToggleVoiceMute(bool mute)
    {
        if (mute) yappingDude.volume = 0;
        else yappingDude.volume = 1;
    }
}
