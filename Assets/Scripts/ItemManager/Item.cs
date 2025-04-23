using UnityEngine;

public enum GroundItemType { 

    // powerup
    Speed, Double, Weapon, Fuel,
    // obstacle
    Barrel, Hole, StopSign,
    None
};

public class Item : MonoBehaviour{

    [SerializeField] public GroundItemType type;

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.CompareTag("Player")){
            Player player = collider.gameObject.GetComponent<Player>();
            switch(type){
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
                default:{
                    if(type != GroundItemType.None){
                        if(player.CanPickUp()){
                            player.PickUp(type);
                            Destroy(gameObject);
                        }
                    }
                    break;
                }
            }
        }
        
    }

}
