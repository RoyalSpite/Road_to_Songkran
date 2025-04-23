using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteBlink : MonoBehaviour
{
    [Header("Blink Settings")]
    public Image targetImage;
    public Sprite normalSprite;
    public Sprite blinkSprite;
    public float blinkDuration = 0.2f;
    public int blinkCount = 5;
    public float intervalBetweenBlinks = 10f; 

    private void Start()
    {
        if (targetImage == null)
        {
            targetImage = GetComponent<Image>();
        }
        StartCoroutine(BlinkCycleRoutine());
    }

    private IEnumerator BlinkCycleRoutine()
    {

        while (true)
        {
            if (intervalBetweenBlinks <= 0)
            {
                intervalBetweenBlinks = 10f; // à«¿¡Ñ¹¾Ñ§
            }

            yield return new WaitForSecondsRealtime(intervalBetweenBlinks);

            for (int i = 0; i < blinkCount; i++)
            {
                targetImage.sprite = blinkSprite;
                yield return new WaitForSecondsRealtime(blinkDuration);

                targetImage.sprite = normalSprite;
                yield return new WaitForSecondsRealtime(blinkDuration);
            }
        }
    }
}
