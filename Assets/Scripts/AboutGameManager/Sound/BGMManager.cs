using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip inGameSound;
    public AudioClip shopSound;
    public AudioClip bossSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("BGMSoundManager: ไม่มี AudioSource ติดอยู่ใน GameObject!");
        }
    }

    // ฟังก์ชันสำหรับเปลี่ยน BGM
    private void ChangeBGM(AudioClip newClip, float volume = 1f)
    {
        if (newClip == null)
        {
            Debug.LogWarning("ChangeBGM: ไม่มี AudioClip ถูกส่งมา!");
            return;
        }

        audioSource.Stop(); // หยุดเพลงเก่า
        audioSource.clip = newClip;
        audioSource.volume = volume;
        audioSource.Play();
    }

    public void ChangeShopBGM()
    { 
        ChangeBGM(shopSound, 0.5f);       
    }

    public void ChangeInGameBGM()
    {
        ChangeBGM(inGameSound, 0.185f);
    }

    public void ChangeBossBGM()
    {
        ChangeBGM(bossSound, 0.3f);
    }
}
