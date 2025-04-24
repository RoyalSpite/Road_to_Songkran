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
            Debug.LogError("AudioSource Null GameObject!");
        }
    }

    private void Start()
    {
        ChangeBGM(inGameSound, 0.185f);
    }

    private void ChangeBGM(AudioClip newClip, float volume = 1f)
    {
        if (newClip == null)
        {
            Debug.LogWarning("ChangeBGM: Not Found AudioClip!");
            return;
        }

        audioSource.Stop(); // ��ش�ŧ���
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
