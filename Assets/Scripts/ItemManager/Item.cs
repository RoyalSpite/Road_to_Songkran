using UnityEngine;

public enum ItemType { None ,Fuel , Speed, Weapon, DoubleScore };

public class Item : MonoBehaviour{

    [SerializeField] public ItemType type;

}
