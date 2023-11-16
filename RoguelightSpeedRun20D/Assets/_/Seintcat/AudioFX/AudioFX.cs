using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFX : MonoBehaviour
{
    [SerializeField]
    private AudioClip clip;

    public void FX()
    {
        AudioPlayer.PlayOneShot(clip);
    }
}
