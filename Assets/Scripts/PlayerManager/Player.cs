using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public int health = 30;

    [SerializeField] private float carSpeed = 15f;

    [SerializeField] private float countDownPowerUp = 0;

    [SerializeField] private ItemType itemType = ItemType.None;

    public Vector3 target = Vector3.zero;

    // Update is called once per frame
    void Update()
    {

        // move
        if(target != Vector3.zero){

            float step =  carSpeed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target, step);

            // Check if the position of the cube and sphere are approximately equal.
            if (Vector3.Distance(transform.position, target) < 0.001f){
                // Swap the position of the cylinder.
                target = Vector3.zero;
            }

        }
        
    }

    private void OnCollisionEnter2D(Collision2D collider){
 
        if(collider.gameObject.CompareTag("Item")){
            itemType =  collider.gameObject.GetComponent<Item>().type;
            // collider.gameObject.GetComponent<Item>().GetPowerUp();

            if(itemType == ItemType.Fuel){
                health = Mathf.Min(30, health + 7);
                itemType = ItemType.None;
            }

            Destroy(collider.gameObject);
        }

    }
}
