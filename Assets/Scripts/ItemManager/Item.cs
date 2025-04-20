using UnityEngine;

public enum GroundItemType { 

    // timed powerup
    Speed, DoubleScore,
    // instant use
    Fuel, 
    // obstacle
    Barrel, Hole, StopSign
};

public class Item : MonoBehaviour{

    [SerializeField] public GroundItemType type;

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.CompareTag("Player")){
            Player player = collider.gameObject.GetComponent<Player>();
            switch(type){
                // for item
                case GroundItemType.Fuel:{
                    player.health = Mathf.Min(
                        30, player.health + 7
                    );
                    Destroy(gameObject);
                    break;
                }
                case GroundItemType.Speed:{
                    Destroy(gameObject);
                    break;
                }
                case GroundItemType.DoubleScore:{
                    player.scoreMultiplier = 2;
                    Destroy(gameObject);
                    break;
                }
                // for obstacle
                case GroundItemType.Barrel:{
                    player.health -= 5;
                    Destroy(gameObject);
                    break;
                }
                case GroundItemType.Hole:{
                    
                    break;
                }
                case GroundItemType.StopSign:{
                    Destroy(gameObject);
                    break;
                }
            }
        }
        
    }

}
