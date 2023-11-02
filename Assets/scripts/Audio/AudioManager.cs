using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public enum SoundGroup{
        None = 0,
        Master,
        Music,
        SFX

    }


[SerializeField] private AudioContainer audioData;
[SerializeField] private AudioMixer mixer;

private void Start(){
    float masterVolume = LoadVolume(SoundGroup.Master);
    float musicVolume = LoadVolume(SoundGroup.Music);
    float sfxVolume = LoadVolume(SoundGroup.SFX);

    SetVolume(SoundGroup.Master, masterVolume);
    SetVolume(SoundGroup.Music, musicVolume);
    SetVolume(SoundGroup.SFX, sfxVolume);
}

private float LoadVolume(SoundGroup group){
    string volumeName = audioData.GetVolumeParamName(group);
    if (volumeName != null){
        return PlayerPrefs.GetFloat(volumeName, audioData.GetDefaultVolume(group));
    }
    return -1;
}

/// <summary>
/// Playes and audio effect for the requested type
/// </summary>
/// <param name="source"> the Audio Source</param>
/// <param name="effectType"> the type of effect that shuold be played</param>
/// <returns>returns the length of the audioclip to be played. -1 if no sutable clips is found.</returns>
    public float PlaySoundEffect(AudioSource source, SoundEffectType effectType){
        AudioClip clip = audioData.GetAudioClipByType(effectType);
        if (clip != null){
            source.PlayOneShot(clip);
            return clip.length;
        }
        return -1f;
    }
/// <summary>
/// Returns the volume normalised to the range of [0,1]
/// </summary>
/// <param name="group">The sound group volume is requested for</param>
/// <returns> The saved normalized volume . If sound group is not valid will return -1 </returns>
    public float GetVolume(SoundGroup group){
        string volumeParamName = audioData.GetVolumeParamName(group);
        
        if (volumeParamName != null && mixer.GetFloat(volumeParamName, out float volume)){
            float normalized = ToLinear(volume);
            //Debug.Log(volume + " : "+ normalized);
            return normalized;
        }
        return -1;
    }
    public bool SetVolume(SoundGroup group, float normalised){
        string volumeParamName = audioData.GetVolumeParamName(group);
        if (volumeParamName != null && mixer.SetFloat(volumeParamName, normalised)){
            PlayerPrefs.SetFloat(volumeParamName, normalised);
            return true;
        }
        return false; 
    }

    public float ToDB(float linear){
        if (linear <= 0) {
            return -80.0f;
        }
        return Mathf.Log10(linear) * 20.0f;
    }
    public float ToLinear(float volume){
        return Mathf.Clamp01(Mathf.Pow(10.0f, volume / 20.0f));
    }

}
