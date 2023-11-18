using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    void Start()
    {
        ParticleSystem[] particles = GetComponentsInChildren<ParticleSystem>();

        float maxDuration = 0;
        foreach (var particle in particles)
        {
            if (particle.main.duration > maxDuration)
            {
                maxDuration = particle.main.duration;
            }
        }

        Destroy(gameObject, maxDuration);
    }
}
