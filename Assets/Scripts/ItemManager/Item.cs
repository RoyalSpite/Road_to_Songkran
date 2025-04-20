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
                    player.carSpeedMultiplier = 1.25f;
                    player.speedCountDown = 3f;
                    Destroy(gameObject);
                    break;
                }
                case GroundItemType.DoubleScore:{
                    player.scoreMultiplier = 2;
                    player.scoreCountDown = 3f;
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
                    player.carSpeedMultiplier = 0.75f;
                    player.speedCountDown = 3f;
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
