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
        
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
