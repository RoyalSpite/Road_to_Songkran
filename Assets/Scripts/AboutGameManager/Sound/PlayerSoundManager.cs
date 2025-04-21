using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    [Header("Audio Source")]
    private AudioSource audioSource;

    [Header("Player Sounds")]
    public AudioClip shootSound;
    public AudioClip hurtSound;
    public AudioClip deathSound;
    public AudioClip pickupItemSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // ฟังก์ชันเรียกเล่นเสียงตามประเภท
    public void PlayShootSound()
    {
        PlaySound(shootSound);
    }

    public void PlayHurtSound()
    {
        PlaySound(hurtSound);
    }

    public void PlayDeathSound()
    {
        PlaySound(deathSound);
    }

    public void PlayPickupItemSound()
    {
        PlaySound(pickupItemSound);
    }

    // ฟังก์ชันกลางสำหรับเล่นเสียง
    private void PlaySound(AudioClip clip)
    {
        if (clip == null) return;

        audioSource.PlayOneShot(clip);
    }
}
