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
                    player.health = Mathf.Max(player.health - 5, 0);
                    Destroy(gameObject);
                    break;
                }
                case GroundItemType.Hole:{
                    // One hit kill
                    player.health = 0;
                    break;
                }
                case GroundItemType.StopSign:{
                    player.health = Mathf.Max(player.health - 10, 0);

                    player.carSpeedMultiplier = 0.85f;
                    player.speedCountDown = 3f;
                    Destroy(gameObject);
                    break;
                }
            }
        }
        
    }

}
