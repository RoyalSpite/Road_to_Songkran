using UnityEngine;

public class ParallaxBG : MonoBehaviour
{
    public float scrollSpeed = 4f;
    public float resetPositionX = -20f; 
    public float startPositionX = 20f; 
    public Sprite sprite;

    private Player player;

    private void Start(){
        player = GameObject.Find("Player").GetComponent<Player>();
        Vector2 size = sprite.bounds.size;
        Debug.Log("Sprite Size (World Units): " + size);
    }

    void Update()
    {
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
