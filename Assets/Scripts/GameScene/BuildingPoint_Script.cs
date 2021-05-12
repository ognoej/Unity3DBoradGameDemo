using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPoint_Script : MonoBehaviour {
    
    // 소유주 1~4
    public int OwnerPlayer = 0;
    public bool isTownOn = false;

    public string Owner_Player_Name = "";

    //SerializeField]
    //GameObject building_prefab;
    public bool Ocupied = false;

    GameObject[] Town_prefabs;
    GameObject[] Castle_prefabs;

    GameObject Town_Alpha;
   public GameObject Town;
    GameObject Castle_Alpha;
   public GameObject Castle;

    [SerializeField]
    GameObject gameDirec;


    public int num;
    

    private void Awake()
    {

        Town_prefabs = new GameObject[2];
        Castle_prefabs = new GameObject[2];
        Town_prefabs[0] = Resources.Load("Prefabs/TownAlpha") as GameObject;
        Town_prefabs[1] = Resources.Load("Prefabs/Town") as GameObject;
        Castle_prefabs[0] = Resources.Load("Prefabs/CastleAlpha") as GameObject;
        Castle_prefabs[1] = Resources.Load("Prefabs/Castle") as GameObject;

        Town_Alpha = Instantiate(Town_prefabs[0]) as GameObject;
        Town_Alpha.transform.SetParent(gameObject.transform);
        Town_Alpha.transform.localPosition = new Vector3(0, 0.1f, 0);
        Town_Alpha.SetActive(false);

        Town = Instantiate(Town_prefabs[1]) as GameObject;
        Town.transform.SetParent(gameObject.transform);
        Town.transform.localPosition = new Vector3(0, 0.1f, 0);
        Town.SetActive(false);

        Castle_Alpha = Instantiate(Castle_prefabs[0]) as GameObject;
        Castle_Alpha.transform.SetParent(gameObject.transform);
        Castle_Alpha.transform.localPosition = new Vector3(0, 0.1f, 0);
        Castle_Alpha.SetActive(false);

        Castle = Instantiate(Castle_prefabs[1]) as GameObject;
        Castle.transform.SetParent(gameObject.transform);
        Castle.transform.localPosition = new Vector3(0, 0.1f, 0);
        Castle.SetActive(false);

    }


    private void Start()
    {
    }
    


    public void Show_TownAlpha()
    {
        if(Ocupied==false)
        {
            Town_Alpha.SetActive(true);
            //Town_Alpha.SetActive(true);
        }
    }

    public void Hide_TownAlpha()
    {
        Town_Alpha.SetActive(false);
    }


    public void Show_CastleAlpha()
    {
        if (Ocupied == false)
        {
            Castle_Alpha.SetActive(true);
        }
    }

    public void Hide_CastleAlpha()
    {
        Castle_Alpha.SetActive(false);
    }



    public void Building_Complete(int _order)
    {

        if (Ocupied == false) // 타운건설일 경우
        {
            Ocupied = true;
            Town_Alpha.SetActive(false);
            Town.SetActive(true);

            //switch(GameDatas.player_number) 차후 수정
            

                var TownMats = Town.transform.Find("Color").GetComponent<MeshRenderer>().materials;      

            switch (_order)
            {
                case 1:
                    {
                        TownMats[0] = Resources.Load("RedTeam") as Material;
                        //OwnerPlayer = GameScene_Director.My_Data.Player_Name;
                        OwnerPlayer = 1;
                        break;
                    }
                case 2:
                    {
                        TownMats[0] = Resources.Load("BlueTeam") as Material;
                      //  OwnerPlayer = GameScene_Director.My_Data.Player_Name;

                        OwnerPlayer = 2;
                        break;
                    }
                case 3:
                    {
                        TownMats[0] = Resources.Load("YellowTeam") as Material;
                       // OwnerPlayer = GameScene_Director.My_Data.Player_Name;

                       OwnerPlayer = 3;
                        break;
                    }
                case 4:
                    {
                        TownMats[0] = Resources.Load("GreenTeam") as Material;
                        //OwnerPlayer = GameScene_Director.My_Data.Player_Name;

                       OwnerPlayer = 4;
                        break;
                    }                    
            }
           Town.transform.Find("Color").GetComponent<MeshRenderer>().materials = TownMats;
            
            
            if(GameObject.Find("GameDirector").GetComponent<GameScene_Director>().My_Order != _order)
            {
                print("sia");
                return;
            }

          //  Town.GetComponentInChildren<MeshRenderer>().materials = TownMats;
            GameObject.Find("GameDirector").GetComponent<GameScene_Director>().SendMessage("TownComplete",gameObject);
        }


        else  // 성 건설일 경우
        {
            Town.SetActive(false);
            Castle_Alpha.SetActive(false);
            Castle.SetActive(true);

            //switch(GameDatas.player_number) 차후 수정

            var CastleMats = Castle.GetComponentInChildren<MeshRenderer>().materials;

            switch (GameScene_Director.now_turn)
            {
                case 1:
                    {
                        CastleMats[1] = Resources.Load("RedTeam") as Material;
                        break;
                    }
                case 2:
                    {
                        CastleMats[2] = Resources.Load("BlueTeam") as Material;
                        break;
                    }
                case 3:
                    {
                        CastleMats[3] = Resources.Load("YellowTeam") as Material;
                        break;
                    }
                case 4:
                    {
                        CastleMats[4] = Resources.Load("GreenTeam") as Material;
                        break;
                    }
            }
            Town.GetComponentInChildren<MeshRenderer>().materials = CastleMats;
            GameObject.Find("GameDirector").GetComponent<GameScene_Director>().SendMessage("BuildComplete");
        }





        /*
        if (OwnerPlayer == 0)
        {
            // 차후 아래와 교체
            //OwnerPlayer = GameObject.Find("GameDirector").gameObject.GetComponent<GameScene_Director>().Player_TurnNumber;

            // 테스트용
            OwnerPlayer = GameScene_Director.now_turn;


            GameObject new_Town = Instantiate(building_prefab) as GameObject;
            new_Town.transform.SetParent(gameObject.transform);
            new_Town.transform.localPosition = new Vector3(0, 0, 0);
            print(GameObject.Find("GameDirector").gameObject.GetComponent<GameScene_Director>().Player_TurnNumber);
            //print(OwnerPlayer);
            switch (OwnerPlayer)
            {
                case 1:
                    {
                        print("siba");
                        new_Town.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material = Resources.Load("RedTeam") as Material;
                        break;
                    }
                case 2:
                    {
                        new_Town.transform.GetChild(1).GetComponent<MeshRenderer>().material = Resources.Load("BlueTeam") as Material;
                        break;
                    }
                case 3:
                    {
                        new_Town.transform.GetChild(1).GetComponent<MeshRenderer>().material = Resources.Load("YellowTeam") as Material;
                        break;
                    }
                case 4:
                    {
                        new_Town.transform.GetChild(1).GetComponent<MeshRenderer>().material = Resources.Load("GreenTeam") as Material;
                        break;
                    }

            }
            Ocupied = true;

            GameObject.Find("GameDirector").SendMessage("Hide_TownSpot");
            return;
        }

        else
        {
            ///  if(OwnerPlayer == GameObject.Find("GameDirector").GetComponent<GameScene_Director>().Player_TurnNumber)
            ///  {
            ///
            ///      //빌딩 만들기                
            ///  }
            print("건물을 지을 수 없습니다");
            return;
        }
        */
    }
    
	
}
