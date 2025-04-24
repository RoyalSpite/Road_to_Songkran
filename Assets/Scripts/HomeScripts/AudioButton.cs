using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
    [SerializeField] private Sprite soundOnIcon;
    [SerializeField] private Sprite soundOffIcon;
    [SerializeField] private Image buttonImage;

    private bool isMuted = false;

    public void ToggleSound()
    {
        isMuted = !isMuted;
        buttonImage.sprite = isMuted ? soundOffIcon : soundOnIcon;
        Debug.Log("Switched!!");

        // ปิดหรือเปิดเสียง
        AudioListener.volume = isMuted ? 0f : 1f;
    }
}
