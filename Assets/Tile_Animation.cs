using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Animation : MonoBehaviour {

    public Animator tile_ani;

    private void Awake()
    {
        tile_ani = GetComponent<Animator>();
    }


    // Use this for initialization
    void Start()
    {

    }

    public void SetAniTransform()
    {
        print("sabls");
        transform.position = new Vector3(0, 0, 0);
    }


	
	// Update is called once per frame
	void Update () {        
		
	}
}
