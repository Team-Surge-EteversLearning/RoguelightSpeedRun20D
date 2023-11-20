using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFX : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> clips;

    public void FX(int index)
    {
        try
        {
            AudioPlayer.PlayOneShot(clips[index]);
        }
        catch 
        {
            Debug.LogError(gameObject.name + ", clips.Count > " + clips.Count + ", index > " + index);
        }
    }
}
