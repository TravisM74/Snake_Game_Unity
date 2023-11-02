using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public enum SoundEffectType {
    None = 0,
    Collect,
    Collide
}

[CreateAssetMenu(fileName = "AudioData", menuName = "Data/AudioContainer")]
public class AudioContainer : ScriptableObject
{

[Serializable] public class VolumeControlName{
    public AudioManager.SoundGroup SoundGroup;
    public string Name;
}

[Serializable] public class SoundItem{
    public SoundEffectType Type;
    public AudioClip[] Clips;
   }

[SerializeField] private SoundItem[] soundClips;
[SerializeField] private VolumeControlName[] volumeNames;

[SerializeField, Range(0,1)] private float defaultMasterVolume;
[SerializeField, Range(0,1)] private float defaultMusicVolume;
[SerializeField, Range(0,1)] private float defaultSFXVolume;

public float DefaultMasterVolume{
    get{return defaultMasterVolume;}
}
public float DefaultMusicVolume{
    get{return defaultMusicVolume;}
}
public float DefaultSFXVolume{
    get{return defaultSFXVolume;}
}

/// <summary>
/// Reuturns a ranndom audio clip for the sound type type. If nmo clips for the type are found 
/// Null will be returned.
/// </summary>
/// <param name="type">the type of requested audio Clip</param>
/// <returns>returns and ausio clip if one can be found for the requested type
/// otherwise Null</returns>
   public AudioClip GetAudioClipByType(SoundEffectType type){
    foreach (SoundItem item in soundClips){
        if (item.Type == type){
            // audio container contains a definition for the audio type.
            if(item.Clips.Length == 0){
                // no audio clips are defined
                continue;
            }
            int randomIndex = Random.Range(0, item.Clips.Length);
            return item.Clips[randomIndex] ;
        }
    }
    return null;
   }

    public string GetVolumeParamName(AudioManager.SoundGroup group){
        foreach (VolumeControlName item in volumeNames){
            if (item.SoundGroup == group){
                return item.Name;
            }
        }
        return null;
    }

    public float GetDefaultVolume(AudioManager.SoundGroup group){
        switch(group)
        {
            case AudioManager.SoundGroup.Master: return DefaultMasterVolume;
            case AudioManager.SoundGroup.Music: return DefaultMusicVolume;
            case AudioManager.SoundGroup.SFX: return DefaultSFXVolume;
            default: return -1;

        }
    }

}
