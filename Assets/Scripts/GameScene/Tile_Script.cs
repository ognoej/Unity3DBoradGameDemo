using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Script : MonoBehaviour {

    // 1: filed 2: pasture 3: forest 4: mountain 5: hills 6: dessert
    public int tile_property = 0;
    public int tile_number = 0;
    public bool is_thiefOn = false;
    public int num;


    public GameObject tile;

    [SerializeField]
    public GameObject[]    nearBillage;
    [SerializeField]
    public GameObject[]    nearRoad;

    private void Awake()
    {
        tile = transform.GetChild(0).gameObject;     
    }   


}