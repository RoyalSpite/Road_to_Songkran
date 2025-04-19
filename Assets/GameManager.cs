using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Player player;
    [SerializeField] private Transform[] roadPosition;

    // [SerializeField] private TextMeshProUGUI ScoreText;
    // [SerializeField] private TextMeshProUGUI FuelText;

    [SerializeField] private int laneIndex = 0;
    [SerializeField] private int colIndex = 0;

    void Start()
    {
        player.transform.position = new Vector3(
            player.transform.position.x,
            roadPosition[laneIndex].position.y
        );
    }

    // Update is called once per frame
    void Update(){

        // FuelText.SetText("HP : " + player.health);
        
        // player movement
        if(player.target == Vector3.zero){

            player.target = player.transform.position;

            if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)){
                // up and down

                if(Input.GetKeyDown(KeyCode.W)){
                    laneIndex = Mathf.Max(laneIndex - 1, 0);
                }
                else if(Input.GetKeyDown(KeyCode.S)){
                    laneIndex = Mathf.Min(laneIndex + 1, roadPosition.Length - 1);
                }

                player.target.y = roadPosition[laneIndex].position.y;
                
            }
            else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A)){
                // left and right
                if(Input.GetKeyDown(KeyCode.D)){
                    if(colIndex < 3){
                        player.target.x += 1;
                        colIndex += 1;
                    }
                }
                else if(Input.GetKeyDown(KeyCode.A)){
                    if(colIndex > 0){
                        player.target.x += -1;
                        colIndex -= 1;
                    }
                }

            }

        }

    }
}
