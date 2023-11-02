using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsController : MonoBehaviour
{
    [SerializeField] VolumeControl masterVolumeControl;
    [SerializeField] VolumeControl musicVolumeControl;
    [SerializeField] VolumeControl sfxVolumeControl;

    public void Save(){
        GameManager.Instance.AudioManager.SetVolume(AudioManager.SoundGroup.Master, masterVolumeControl.Value);
        GameManager.Instance.AudioManager.SetVolume(AudioManager.SoundGroup.Music, musicVolumeControl.Value);
        GameManager.Instance.AudioManager.SetVolume(AudioManager.SoundGroup.SFX, sfxVolumeControl.Value);
        
    }

    public void Close(){
        GameManager.Instance.GoBack();
    }

    public void ExitMenu(){
        GameManager.Instance.Go(GameStateBase.Type.Menu);
    }
}
