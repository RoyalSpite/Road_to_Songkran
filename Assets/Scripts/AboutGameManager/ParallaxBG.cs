using UnityEngine;

public class ParallaxBG : MonoBehaviour
{
    public float scrollSpeed = 2f;    // �������Ƿ��������͹
    public float resetPositionX = -20f; // �ش��������絡�Ѻ������
    public float startPositionX = 20f; // �ش������鹢ͧ���

    private Player player;

    private void Start(){
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        transform.Translate(
            Vector3.left * scrollSpeed * Time.deltaTime * player.baseCarSpeedMultiplier
        );

        if (transform.position.x <= resetPositionX)
        {
            Vector3 newPos = transform.position;
            newPos.x = startPositionX;
            transform.position = newPos;
        }
    }
}
