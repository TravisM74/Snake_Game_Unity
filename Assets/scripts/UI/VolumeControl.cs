using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
  [SerializeField] private Slider slider;
  [SerializeField] private AudioManager.SoundGroup targetGroup;

  public float Value{
    get{return slider.value;}

  }

  private void Start()
    {
        SetVolume();
    }

    private void SetVolume()
    {
        float normalised = GameManager.Instance.AudioManager.GetVolume(targetGroup);
        if (normalised < 0)
        {
            Debug.LogError("Error cant read the volume for the Mixergroup " + targetGroup);
            return;
        }
        slider.value = normalised;
    }
}
