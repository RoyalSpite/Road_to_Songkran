using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { None ,Fuel , Speed, Weapon, DoubleScore };

public class Item : MonoBehaviour{

    [SerializeField] public ItemType type;

    // Update is called once per frame
    public void GetPowerUp(){
        print(type);
    }

}
