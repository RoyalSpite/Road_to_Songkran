using UnityEngine;

public class ParallaxBG : MonoBehaviour
{    
    public static float baseScrollSpeed = 4f;
    public static float scrollSpeed = 4f;

    public float resetPositionX = -20f; 
    public float startPositionX = 20f; 

    void Update()
    {
        if(GameManager.isGameOver) return;
        transform.Translate(
            Vector3.left * scrollSpeed * Time.deltaTime
        );

        if (transform.position.x <= resetPositionX)
        {
            Vector3 newPos = transform.position;
            newPos.x = startPositionX;
            transform.position = newPos;
        }
    }
}
