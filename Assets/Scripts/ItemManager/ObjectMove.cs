using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    public float moveSpeed = 5f;

    private void Start()
    {
        Destroy(gameObject, 5);
    }

    void Update()
    {
        if(GameManager.isGameOver) return;
        
        transform.Translate(
            moveSpeed * ParallaxBG.scrollSpeed * Time.deltaTime * Vector3.left
        );
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
