using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFX : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> clips;

    public void FX(int index)
    {
        AudioPlayer.PlayOneShot(clips[index]);
    }
}
