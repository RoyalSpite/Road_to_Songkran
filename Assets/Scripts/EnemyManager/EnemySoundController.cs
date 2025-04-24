using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundController : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] private AudioSource audioSource;

    [Header("Clips")]
    [SerializeField] private AudioClip shootClip;
    [SerializeField] private AudioClip hitClip;

    public void PlayShootSound()
    {
        PlaySound(shootClip);
        Debug.Log("Shoot");
    }

    public void PlayHitSound()
    {
        PlaySound(hitClip);
        Debug.Log("Dead");
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
