using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDatass : MonoBehaviour {

  // static public string Player_Name;
  // static public int[] Resources;

   // public int[] Test_Player_Resources;
   // public int Test_Player_Number;
   // public string Test_Player_Name;

   public int Room_Token = 0;
   public int Player_Order = 0;
    //public int[] Player_Resources;
    public string Player_Name = "스발롬";

    private void Start()
    {       
        Player_Name = System.String.Empty;       
        Player_Name = "홀리머더";
      //  Player_Resources = new int[5];
    }

   public int Grain = 0;
   public int Lumber = 0;
   public int Brick = 0;
   public int Ore = 0;
   public int Wool = 0;  

}
