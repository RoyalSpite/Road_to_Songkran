using UnityEngine;

public class ParallaxBG : MonoBehaviour
{    
    public static readonly float baseScrollSpeed = 4f;
    public static float scrollSpeed = 4f;

    public float resetPositionX = -20f; 
    public float startPositionX = 20f; 

    private Player player;

    private void Start(){
        player = GameObject.Find("Player").GetComponent<Player>();
    }

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
