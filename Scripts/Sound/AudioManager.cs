using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public SoundDetailsList_SO soundDetailsList_SO;

    public SoundDetails GetSoundDetailsBySoundName(SoundName soundName)
    {
        return soundDetailsList_SO.GetSoundDetails(soundName);
    }
}
