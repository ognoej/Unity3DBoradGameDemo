using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadPoint_Script : MonoBehaviour
{

    public int OwnerPlayer = 5;

    [SerializeField]
    GameObject[] Road_Prefab;

    public int num;

    GameObject Road_Alpha;
   public  GameObject Road;


    [SerializeField]
    GameObject gameDirec;


    public bool Ocupied = false;

    private void Awake()
    {
        Road_Prefab = new GameObject[2];
        Road_Prefab[0] = Resources.Load("Prefabs/RoadAlpha") as GameObject;
        Road_Prefab[1] = Resources.Load("Prefabs/Road") as GameObject;

        Road_Alpha = Instantiate(Road_Prefab[0]) as GameObject;
        Road_Alpha.transform.SetParent(gameObject.transform);
        Road_Alpha.transform.localPosition = new Vector3(0, 0.1f, 0);
        Road_Alpha.transform.localRotation = Quaternion.AngleAxis(-90, Vector3.up);
        Road_Alpha.SetActive(false);

        Road = Instantiate(Road_Prefab[1]) as GameObject;
        Road.transform.SetParent(gameObject.transform);
        Road.transform.localPosition = new Vector3(0, 0.1f, 0);
        Road.transform.localRotation = Quaternion.AngleAxis(-90, Vector3.up);
        Road.SetActive(false);
    }


    private void Start()
    {
       

    }

    public void Show_RoadAlpha()
    {
        if (Ocupied == false)
        {
            Road_Alpha.SetActive(true);
        }
    }

    public void Hide_Road_TownAlpha()
    {
        Road_Alpha.SetActive(false);
    }


    public void Building_Complete(int _order)
    {
        if (Ocupied == false) // 타운건설일 경우
        {
            Ocupied = true;
            Road_Alpha.SetActive(false);
            Road.SetActive(true);

            //switch(GameDatas.player_number) 차후 수정

            var RoadMats = Road.transform.Find("Color").GetComponent<MeshRenderer>().materials;

            switch (_order)
            {
                case 1:
                    {
                        RoadMats[0] = Resources.Load("RedTeam") as Material;
                        OwnerPlayer = 1;
                        break;
                    }
                case 2:
                    {
                        RoadMats[0] = Resources.Load("BlueTeam") as Material;
                        OwnerPlayer = 2;
                        break;
                    }
                case 3:
                    {
                        RoadMats[0] = Resources.Load("YellowTeam") as Material;
                        OwnerPlayer = 3;
                        break;
                    }
                case 4:
                    {
                        RoadMats[0] = Resources.Load("GreenTeam") as Material;
                        OwnerPlayer = 4;
                        break;
                    }
            }
            Road.transform.Find("Color").GetComponent<MeshRenderer>().materials = RoadMats;

            if (GameObject.Find("GameDirector").GetComponent<GameScene_Director>().My_Order != _order)
            {
                return;
            }
            //Road.GetComponentInChildren<MeshRenderer>().materials = RoadMats;
            GameObject.Find("GameDirector").GetComponent<GameScene_Director>().SendMessage("RoadComplete",gameObject);
        }


        /*
        if (OwnerPlayer == 0)
        {
            // 차후 교체
            // OwnerPlayer = GameObject.Find("GameDirector").gameObject.GetComponent<GameScene_Director>().Player_TurnNumber;
            // 테스트용
            OwnerPlayer = GameScene_Director.now_turn;

            GameObject new_Road = Instantiate(Road_Prefab) as GameObject;
            new_Road.transform.SetParent(gameObject.transform);
            new_Road.transform.localPosition = new Vector3(0, 0, 0);
            new_Road.transform.localRotation = Quaternion.AngleAxis(-90, Vector3.up);


            switch (OwnerPlayer)
            {
                case 1:
                    {
                        new_Road.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material = Resources.Load("RedTeam") as Material;
                        break;
                    }
                case 2:
                    {
                        new_Road.transform.GetChild(1).GetComponent<MeshRenderer>().material = Resources.Load("BlueTeam") as Material;
                        break;
                    }
                case 3:
                    {
                        new_Road.transform.GetChild(1).GetComponent<MeshRenderer>().material = Resources.Load("YellowTeam") as Material;
                        break;
                    }
                case 4:
                    {
                        new_Road.transform.GetChild(1).GetComponent<MeshRenderer>().material = Resources.Load("GreenTeam") as Material;
                        break;
                    }
            }
            Ocupied = true;
            GameObject.Find("GameDirector").SendMessage("Hide_RoadSpot");
            return;
        }
    */
    }
}
     