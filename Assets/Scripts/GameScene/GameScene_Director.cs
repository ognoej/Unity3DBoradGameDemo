using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System;


public class GameScene_Director : MonoBehaviour {


    // other users datas
    public static GameDatass My_Data;
    public GameDatass Others;

    // 1: filed 2: pasture 3: forest 4: mountain 5: hills 6: dessert
    [SerializeField]
    GameObject[] tilemaps;

    [SerializeField]
    GameObject Map_All;

    [SerializeField]
    GameObject Token_Prefab;
    
    [SerializeField]
    GameObject StartBtn;

    [SerializeField]
    GameObject MenuBtn;

    [SerializeField]
    GameObject TopUI_Bar;

    [SerializeField]
    GameObject Bottom_UIBar;

    [SerializeField]
    GameObject Bottom_UIBtn;

    [SerializeField]
    GameObject ChangeViewBtn;

    [SerializeField]
    GameObject ServerConnectBtn;

    [SerializeField]
    GameObject[] OceanList;

    [SerializeField]
    Text TopUI_Text;

    [SerializeField]
    GameObject m_Game_server;

    GameScene_Network m_server;   


    [SerializeField]
    GameObject TurnEndBtn;


    [SerializeField]
    GameObject Dice1;
    [SerializeField]
    GameObject Dice2;
    [SerializeField]
    GameObject DiceBtn;

    [SerializeField]
    Text[] MyResources;
    //wool meat ore lumber brick
    // 메세지 큐
    MessageQueue Messagequeue;
    
    // Map 관련
    List<GameObject> Map_List;
    bool isMapOn = false;
    bool isMenuToggleOn = false;

    // 로컬 
    public int Player_Score;
    public int Player_TurnNumber = 0;
    public int My_Order = 0;
    public int TotalPlayer_number = 0;

    // 타이머용 시간
    float g_time = 0f;


    // 테스트용
    public static int now_turn = 1;
   // GameDatass[] persons;

    // UI관련
    bool isBottomUI_On = false;


    bool isMaster = false;
    bool isConnected = false;

    bool isSettingRoundCycle = false;

    // 클라
    MyTCPClient m_client;

    // 메세지
    string msg = "";

    public bool isBuild = false;
    // 코루틴 재시작용
    IEnumerator m_Recorutine;

    private void Awake()
    {
        m_server = m_Game_server.GetComponent<GameScene_Network>();
        m_Recorutine = CheckQueue();
        Messagequeue = MessageQueue.GetInstance;
        StartCoroutine(m_Recorutine);
    }

    GameObject[] builspot;
    GameObject[] roadspot;

    // Use this for initialization
    void Start()
    {
        Map_List = new List<GameObject>();
        Map_List.AddRange(tilemaps);

        int k = 0;
        //  foreach(var j in Map_List)
        //  {
        //      j.GetComponent<Tile_Script>().num = k;
        //      k++;
        //      int l = 0;
        //      foreach(var m in j.GetComponent<Tile_Script>().nearBillage)
        //      {
        //          m.GetComponent<BuildingPoint_Script>().num = l;
        //          l++;
        //      }
        //      int u = 0;
        //      foreach (var n in j.GetComponent<Tile_Script>().nearRoad)
        //      {
        //          n.GetComponent<RoadPoint_Script>().num = u;
        //          u++;
        //      }
        //  }

        builspot = GameObject.FindGameObjectsWithTag("builspot");
    
        foreach(var j in builspot)
        {
           
            j.GetComponent<BuildingPoint_Script>().num = k;
            k++;
        }
        k = 0;

        roadspot = GameObject.FindGameObjectsWithTag("roadspot");

        foreach (var j in roadspot)
        {
            
            j.GetComponent<RoadPoint_Script>().num = k;
            k++;
        }
        k = 0;

        My_Data = new GameDatass();
        Others = new GameDatass();
        var i = UnityEngine.Random.Range(0, 6);
        switch(i)
        {
            case 0:
                {
                    My_Data.Player_Name = "임꺽정";
                    break;
                }
            case 1:
                {
                    My_Data.Player_Name = "홍길동";
                    break;
                }
            case 2:
                {
                    My_Data.Player_Name = "전우치";
                    break;
                }
            case 3:
                {
                    My_Data.Player_Name = "별주부";
                    break;
                }
            case 4:
                {
                    My_Data.Player_Name = "용왕";
                    break;
                }
            case 5:
                {
                    My_Data.Player_Name = "토끼";
                    break;
                }

            case 6:
                {
                    My_Data.Player_Name = "곰";
                    break;
                }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "townAlpha")
                    {
                        //print(hit.collider.gameObject.transform.parent.name);
                        hit.collider.gameObject.transform.parent.GetComponent<BuildingPoint_Script>().Building_Complete(My_Order);
                    }
                    else if (hit.collider.tag == "RoadAlpha")
                    {
                        hit.collider.gameObject.transform.parent.GetComponent<RoadPoint_Script>().Building_Complete(My_Order);
                    }
                }
            }        

    }



        public void ReCorutine()
    {       
        //print("코루틴 재시작 한다");
        StopCoroutine(m_Recorutine);
        m_Recorutine = CheckQueue();
        StartCoroutine(m_Recorutine);
    }

    private IEnumerator CheckQueue()
    {
        WaitForSeconds waitSec = new WaitForSeconds(1);

        while(true)
        {
            string data = Messagequeue.GetData();
            if(!data.Equals(string.Empty))
            {
                print(data);
                Receivemsg(data);                
                ReCorutine();
                yield break;
            }
          //  print("받는중");
            yield return waitSec;
        }        
    }


    bool isSettingRound = true;
    bool isFirstMessage = false;
  
    public void Receivemsg(string _msg)
    {
        print(_msg);
    
        
        //if (isFirstMessage == false) //&& m_Game_server.GetComponent<GameScene_Network>().isMaster==true)
        //{
        //    //m_room_key = e.Data;
        //    isFirstMessage = true;
        //    print("Bangjang");
        //    return;
        //}
        if (_msg[0] !='{')
        {
            return;
        }

        JsonClass new_msg = JsonUtility.FromJson<JsonClass>(_msg);

        print("메세지 도착");
        print(new_msg.function);
        switch (new_msg.function)
        {           
            case "Start":
                {
                    if (isMaster == true)
                    {                      
                        print("랜덤 맵 적용 완료");
                        print_tileInfo();
                        SendName();
                        GameStart();
                    }
                    else
                    {                       
                        string[] splitmsg = new_msg.Map_Info.Split(' ');
                        int j = 0;
                        foreach (var i in Map_List)
                        {
                            i.GetComponent<Tile_Script>().tile_number = Convert.ToInt32(splitmsg[j]);
                            j++;
                            i.GetComponent<Tile_Script>().tile_property = Convert.ToInt32(splitmsg[j]);
                            j++;
                        }
                        print("손님 게임 시작");
                        My_Order = 2;
                        SendName();
                        GameStart();
                    }

                    // print("Start 메세지 받았다");
                    //GameStart();
                    break;
                }
            case "Name":
                {
                    if(new_msg.Order!=My_Order)
                    {
                        new_msg.id = Others.Player_Name;
                    }
                    break;
                }

            case "Setting":
                {
                    print("세팅메세지 도착");
                    if(My_Order==1)
                    {
                        BeginMyTurn();
                        //Invoke("Show_BuildingSpot", 4.0f);
                    }
                    break;
                }
            case "TurnStart":
                {
                    TopUI_Text.text = new_msg.id + " 님의 차례입니다.";                    
                    break;
                }
            case "TurnEnd":
                {
                    if(My_Order == new_msg.Order)
                    {
                        BeginMyTurn();
                    }
                    break;
                }

            case "Dice":
                {
                    print(new_msg.resource + " 가 나왔습니다");
                    
                    foreach(var i in Map_List)
                    {                        
                        var bils = i.GetComponent<Tile_Script>();
                        if (bils.tile_number == Convert.ToInt32(new_msg.resource))
                        {
                            foreach (var j in bils.nearBillage)
                            {
                                var points = j.GetComponent<BuildingPoint_Script>();
                                if (points.OwnerPlayer == My_Order)
                                {
                                    getResources(bils.tile_property, My_Order);
                                }
                            }
                        }
                    }
                    break;
                }
            // 1: filed 2: pasture 3: forest 4: mountain 5: hills 6: dessert
            case "Resource":
                {
                    //wool meat ore lumber brick
                    if (new_msg.Order == My_Order)
                    {
                        switch(Convert.ToInt32(new_msg.resource))
                        {
                            case 1:
                                {
                                    
                                    //MyResources[1].text += 1;
                                    My_Data.Grain += 1;
                                    gainresource(1);
                                    break;
                                }
                            case 2:
                                {
                                    //MyResources[0].text += 1;
                                    My_Data.Wool += 1;
                                    gainresource(2);
                                    break;
                                }
                            case 3:
                                {
                                   // MyResources[3].text += 1;
                                    My_Data.Lumber += 1;
                                    gainresource(3);
                                    break;
                                }
                            case 4:
                                {
                                   // MyResources[2].text += 1;
                                    My_Data.Ore += 1;
                                    gainresource(4);
                                    break;
                                }
                            case 5:
                                {
                                   // MyResources[4].text += 1;
                                    My_Data.Brick += 1;
                                    gainresource(5);
                                    break;
                                }
                        }
                    }
                    else
                    {
                        switch (Convert.ToInt32(new_msg.resource))
                        {
                            case 1:
                                {
                                    Others.Grain += 1;
                                    break;
                                }
                            case 2:
                                {
                                    Others.Wool += 1;
                                    break;
                                }
                            case 3:
                                {
                                    Others.Lumber += 1;
                                    break;
                                }
                            case 4:
                                {
                                    Others.Ore += 1;
                                    break;
                                }
                            case 5:
                                {
                                    Others.Brick += 1;
                                    break;
                                }
                        }
                    }
                    break;
                }

            case "Town":
                {
                    if(new_msg.Order == My_Order)
                    {
                        return;
                    }

                    string[] splitmsg = new_msg.resource.Split(' ');

                    int i = Convert.ToInt32(splitmsg[0]);
                    int j = Convert.ToInt32(splitmsg[1]);
                    
                    //spot.Ocupied = true;
                    builspot[j].GetComponent<BuildingPoint_Script>().OwnerPlayer = new_msg.Order;
                    builspot[j].GetComponent<BuildingPoint_Script>().Building_Complete(new_msg.Order);
                    //spot.
                    break;
                }
            case "Road":
                {
                   //if (new_msg.Order == My_Order)
                   //{
                   //    return;
                   //}
                    string[] splitmsg = new_msg.resource.Split(' ');

                    int i = Convert.ToInt32(splitmsg[0]) ;
                    int j = Convert.ToInt32(splitmsg[1]) ;

                    print(i+" " +j);
                    //var spot = Map_List[i].GetComponent<Tile_Script>().nearRoad[j].GetComponent<RoadPoint_Script>();
                    //spot.Ocupied = true;
                    roadspot[j].GetComponent<RoadPoint_Script>().OwnerPlayer = new_msg.Order;
                    roadspot[j].GetComponent<RoadPoint_Script>().Building_Complete(new_msg.Order);
                    //spot.
                    break;
                }

        }        
    }

    

    public void gainresource(int _property)
    {
        switch(_property)
        {
            case 1:
                {
                    MyResources[1].text = Convert.ToString(My_Data.Grain);
                    break;
                }
            case 2:
                {
                    MyResources[0].text = Convert.ToString(My_Data.Grain);
                    //Others.Wool += 1;
                    break;
                }
            case 3:
                {
                    MyResources[3].text = Convert.ToString(My_Data.Grain);
                    //Others.Lumber += 1;
                    break;
                }
            case 4:
                {
                    MyResources[2].text = Convert.ToString(My_Data.Grain);
                    //Others.Ore += 1;
                    break;
                }
            case 5:
                {
                    MyResources[4].text = Convert.ToString(My_Data.Grain);
                   // Others.Brick += 1;
                    break;
                }
        }
    }
    public void SendName()
    {
        var json = new JsonClass();
        json.function = "Name";
        json.id = My_Data.Player_Name;
        json.Order = My_Order;
        m_server.Sendmsg(json);
    }


    public void getResources(int _property,int _owner)
    {
        var resource_message = new JsonClass();
        resource_message.function = "Resource";
        resource_message.Order = _owner;
        resource_message.resource = Convert.ToString(_property);
        m_server.Sendmsg(resource_message);
    }

    int setround_count = 0;

    public void SetRoundBegin()
    {
        
        setround_count++;

        if (setround_count > 1)
        {
            isSettingRound = false;
        }
        Invoke("Show_BuildingSpot",2.0f);
    }






    private void BeginMyTurn()
    {
       if(isSettingRound == true)
        {
            SetRoundBegin();
            return;
        }

        var json = new JsonClass();
        json.id = My_Data.Player_Name;
        json.function = "TurnStart";
        m_Game_server.GetComponent<GameScene_Network>().Sendmsg(json);
        TurnEndBtn.SetActive(true);


        DiceBtn.SetActive(true);
     
    }





    int diceresult = 0;

    public void RollTheDice()
    {
        Dice1.SetActive(true);
        Dice2.SetActive(true);
        DiceBtn.SetActive(false);

        var dice1 = Dice1.GetComponent<Dice>();
        var dice2 = Dice2.GetComponent<Dice>();

        dice1.RollResult();
        dice2.RollResult();

        Invoke("dicegg", 3.0f);
        return;// dice1.result + dice2.result;
    }

    public void dicegg()
    {
        var result = Dice1.GetComponent<Dice>().result + Dice2.GetComponent<Dice>().result;
        print(result);



        var dice_message = new JsonClass();
        dice_message.function = "Dice";
        dice_message.id = My_Data.Player_Name;
        dice_message.Order = My_Order;
        dice_message.resource = Convert.ToString(result);

        m_server.Sendmsg(dice_message);       


        Dice1.SetActive(false);
        Dice2.SetActive(false);


    }


    private void Move_Theif()
    {
    }

    public int Roll_Dice()
    {
        int dice = UnityEngine.Random.Range(2, 12);
        return dice;
    }



    void SetOrder(int _order)
    {
        My_Order = _order;
        return;
    }
 

 
    public void MyTurnEnd_SettingRound()
    {
        var json = new JsonClass();
        json.function = "TurnEnd";
        if(My_Order==1)
        {
            json.Order = My_Order +1;
        }
        else
        {
            json.Order = My_Order-1;
        }
        json.id = My_Data.Player_Name;
        m_Game_server.GetComponent<GameScene_Network>().Sendmsg(json);
        TurnEndBtn.SetActive(false);
    }


    public void MasterStart()
    {
        isMaster = true;
        My_Order = 1;
        CreateMaps();
        //GameStart();
    }


    /////////////////////////////////////////////////////////////////////////
    //////////////////////// 게임 시작  //////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// GameStart()
    /// 게임 시작 함수
    /// </summary>
    public void GameStart()
    {
        TopUI_Text.text = "게임을 시작합니다.";

        StartBtn.SetActive(false);
        ServerConnectBtn.SetActive(false);
        ChangeViewBtn.SetActive(true);
        TopUI_Bar.SetActive(true);
        //Bottom_UIBtn.SetActive(false);
        Bottom_UIBar.SetActive(true);

        if (isMapOn == true)
        {
            isMapOn = false;
            Map_All.SetActive(false);
            return;
        }

        Map_All.SetActive(true);
        isMapOn = true;

        InvokeRepeating("Tile_Drop", 1.0f, 0.15f);

        if (isMaster == true)
        {
           // change_material();
            Invoke("Setting_Round", 9.0f);            
        }

        else
        {
            change_material();
        }    

    }
    

    /// <summary>
    /// Setting_Round()
    /// 세팅 라운드에서 턴시작과 동일한 함수
    /// 서버 할일  : 전체 사용자에게 이 함수 콜을 보냄.
    /// </summary>
    /// <param name
    ///      ></param>
    public void Setting_Round()
    {
        var json = new JsonClass();
        json.function = "Setting";        
        m_Game_server.GetComponent<GameScene_Network>().Sendmsg(json);
        print(json.function + "메세지 보냄");
    }

    /// <summary>
    /// CreateMaps()
    /// 맵 생성 함수 (마스터/로컬)
    /// </summary>    
    public void CreateMaps()
    {              
        // 숫자 섞기
        int[] number = { 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12 };
        int temp_num = 0;
        int ran_int, ran_int2;
        for (int i = 0; i < 18 * 3; i++)
        {
            ran_int = UnityEngine.Random.Range(0, 19); // 0~18  0이상 19미만정수
            ran_int2 = UnityEngine.Random.Range(0, 19);
            temp_num = number[ran_int];
            number[ran_int] = number[ran_int2];
            number[ran_int2] = temp_num;
        }        

        // 맵 정보 섞기
        int[] map_property = { 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 5, 5, 5 };
        // 1: filed 2: pasture 3: forest 4: mountain 5: hills 6: dessert
        for (int i = 0; i < 18 * 3; i++)
        {
            ran_int = UnityEngine.Random.Range(0, 18);
            ran_int2 = UnityEngine.Random.Range(0, 18);
            temp_num = map_property[ran_int];
            map_property[ran_int] = map_property[ran_int2];
            map_property[ran_int2] = temp_num;
        }

        // 맵 리스트에 숫자 정보와 맵 정보 입력
        int temp_num19 = 0, temp_property19 = 0, j = 0;
        foreach(var i in Map_List)
        {
            if( j == 18) // 마지막 배열
            {
                if(temp_num19 == 0) // 마지막 배열전 7이 안나왔을 경우
                {
                    i.GetComponent<Tile_Script>().tile_number = 7;
                    i.GetComponent<Tile_Script>().tile_property = 6;
                }
                else // 마지막 배열전 7이 나왔을 경우
                {
                    i.GetComponent<Tile_Script>().tile_number = temp_num19;
                    i.GetComponent<Tile_Script>().tile_property = temp_property19; 
                }
                m_Game_server.GetComponent<GameScene_Network>().Sendmsg(send_tile_info());
                change_material();
                return;
            }

            // 숫자 7인 타일
            else if(number[j]==7) 
            {
                i.GetComponent<Tile_Script>().tile_number = 7;
                i.GetComponent<Tile_Script>().tile_property = 6;
                temp_num19 = number[18]; 
                temp_property19 = map_property[j];
            }

            // 나머지
            else
            {
                i.GetComponent<Tile_Script>().tile_number = number[j];
                i.GetComponent<Tile_Script>().tile_property = map_property[j];
            }
            j++;            
        }
    }

    /// <summary>
    /// change_material()
    /// 타일 머터리얼 숫자에 맞게 바꾸는 함수(마스터용/로컬)
    /// </summary>    
    private void change_material()
    {
        foreach(var i in Map_List)
        {
            var mats = i.transform.GetChild(0).GetChild(1).GetComponent<MeshRenderer>().materials;           

            switch (i.GetComponent<Tile_Script>().tile_property)
            {                              
                case 1:
                    {          
                        mats[0] = Resources.Load("Fields") as Material;
                        break;
                    }
                case 2:
                    {
                        mats[0] = Resources.Load("Pasture") as Material;
                        break;
                    }
                case 3:
                    {
                        mats[0] = Resources.Load("Forest") as Material;
                        break;
                    }
                case 4:
                    {
                        mats[0] = Resources.Load("Mountain") as Material;
                        break;
                    }
                case 5:
                    {
                        mats[0] = Resources.Load("Hills") as Material;
                        break;
                    }
                case 6:
                    {
                        mats[0] = Resources.Load("Dessert") as Material;
                        break;
                    }
            }// switch         
            
            i.transform.GetChild(0).GetChild(1).GetComponent<MeshRenderer>().materials = mats;

        }// foreach
         
        make_tokens();

    }

    /// <summary>
    /// make_tokens()
    /// 숫자토큰의 머터리얼을 바꿔주는 함수 (마스터/로컬)
    /// </summary>
    private void make_tokens()
    {
        string num_str;
        foreach (var i in Map_List)
        {            
            GameObject child_token = i.transform.Find("Realtoken").gameObject;  
            num_str = i.GetComponent<Tile_Script>().tile_number.ToString();   
            child_token.GetComponent<MeshRenderer>().material = Resources.Load(num_str) as Material;
        }
    }



    //////////////////////////////////////////////////////////////////////////////////////////
    //////////////// 건물 건설 함수 ///////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// Show_BuildingSpot()
    /// 건물 건설 시 건물 지을 수 있는 위치 보여주는 함수 (로컬)
    /// </summary>
    /// <param name="TopUi_Text"는 턴 시작 함수로 차후에 옮김></param>
    public void Show_BuildingSpot() // 건물 놓을자리
    {
        //TopUI_Text.text = "Player" + (now_turn) + " 님의 턴입니다 ";//Others[now_turn].Test_Player_Name + " 님의 차례입니다";

        foreach (var i in Map_List)
        {
            foreach (var j in i.GetComponent<Tile_Script>().nearBillage)
            {
                j.GetComponent<BuildingPoint_Script>().Show_TownAlpha();
            }
        }        
    }


    /// <summary>
    /// TownComplete()
    /// 건물 건설 완료 후 Alpha건물 숨김함수 (로컬) 
    /// </summary>
    bool issend = false;
    public void TownComplete(GameObject _info)
    {
        foreach (var i in Map_List)
        {
            foreach (var j in i.GetComponent<Tile_Script>().nearBillage)
            {
                if(j.gameObject == _info && issend ==false)
                {
                    issend = true;
                    var json = new JsonClass();
                    json.function = "Town";
                    json.id = My_Data.Player_Name;
                    json.Order = My_Order;
                    json.resource += Convert.ToString(i.GetComponent<Tile_Script>().num) + " " + Convert.ToString(j.GetComponent<BuildingPoint_Script>().num);
                    m_server.Sendmsg(json);
                }
                j.GetComponent<BuildingPoint_Script>().Hide_TownAlpha();
            }
        }
        issend = false;

        //isBuild = true;
        Show_RoadSpot();
    }


    /// <summary>
    /// Show_RoadSpot()
    /// 도로 건설 시 도로 건설 가능한 스팟을 표시해주는 함수 (로컬)
    /// </summary>
    public void Show_RoadSpot()
    {
        foreach (var i in Map_List)
        {
            for (int j = 0; j < 6; j++)
            {
                var Owner = i.GetComponent<Tile_Script>().nearBillage;
              //  print(Owner[j].GetComponent<BuildingPoint_Script>().OwnerPlayer);

                if (Owner[j].GetComponent<BuildingPoint_Script>().OwnerPlayer == My_Order)
                {
                    //print("shiaba");
                    switch (j)
                    {
                        case 0:
                            {
                                i.GetComponent<Tile_Script>().nearRoad[0].GetComponent<RoadPoint_Script>().Show_RoadAlpha();
                                i.GetComponent<Tile_Script>().nearRoad[5].GetComponent<RoadPoint_Script>().Show_RoadAlpha();
                                break;
                            }
                        case 1:
                            {
                                i.GetComponent<Tile_Script>().nearRoad[0].GetComponent<RoadPoint_Script>().Show_RoadAlpha();
                                i.GetComponent<Tile_Script>().nearRoad[1].GetComponent<RoadPoint_Script>().Show_RoadAlpha();
                                break;
                            }
                        case 2:
                            {
                                i.GetComponent<Tile_Script>().nearRoad[5].GetComponent<RoadPoint_Script>().Show_RoadAlpha();
                                i.GetComponent<Tile_Script>().nearRoad[4].GetComponent<RoadPoint_Script>().Show_RoadAlpha();
                                break;
                            }
                        case 3:
                            {
                                i.GetComponent<Tile_Script>().nearRoad[1].GetComponent<RoadPoint_Script>().Show_RoadAlpha();
                                i.GetComponent<Tile_Script>().nearRoad[2].GetComponent<RoadPoint_Script>().Show_RoadAlpha();
                                break;
                            }
                        case 4:
                            {
                                i.GetComponent<Tile_Script>().nearRoad[3].GetComponent<RoadPoint_Script>().Show_RoadAlpha();
                                i.GetComponent<Tile_Script>().nearRoad[4].GetComponent<RoadPoint_Script>().Show_RoadAlpha();
                                break;
                            }
                        case 5:
                            {
                                i.GetComponent<Tile_Script>().nearRoad[2].GetComponent<RoadPoint_Script>().Show_RoadAlpha();
                                i.GetComponent<Tile_Script>().nearRoad[3].GetComponent<RoadPoint_Script>().Show_RoadAlpha();
                                break;
                            }
                    }//switch                 
                }//if 
            }//for    
        }//foreach        
    }// show Road Spot


    /// <summary>
    /// RoadComplete()
    /// 도로 건설 완료 함수. AlphaRoad 모두 숨김 (로컬)
    /// </summary>

  //  bool issends = false;
    public void RoadComplete(GameObject _info)
    {
        foreach (var i in Map_List)
        {
            foreach (var j in i.GetComponent<Tile_Script>().nearRoad)
            {
                if (j.gameObject == _info )
                {
                    //issends = true;
                    var json = new JsonClass();
                    json.function = "Road";
                    json.id = My_Data.Player_Name;
                    json.Order = My_Order;
                    json.resource += Convert.ToString(i.GetComponent<Tile_Script>().num) + " " + Convert.ToString(j.GetComponent<RoadPoint_Script>().num);
                    m_server.Sendmsg(json);
                }
                j.GetComponent<RoadPoint_Script>().Hide_Road_TownAlpha();
            }
        }//foreach
       // issends = true;
        MyTurnEnd_SettingRound();        
        //isBuild = false;
    } // RoadComplete


    public void End_SettingRound()
    {
        // 서버에 자신턴 종료 콜 + 건물 도로 위치 전송
        NotMyTurn();
        return;
    } // End_SettingRound

    public void NotMyTurn()
    {
        // 내 턴 아닐 경우 할 수 있는 동작 설정
    }



    /// <summary> (테스트용)
    /// End_Turn()
    /// 턴 종료/턴시작 함수 
    /// </summary>
    /// <param name="now_turn"은 출력확인을 위해 임시로 정한 내부 순서값.
    /// 차후 EndTurn()함수로 교체
    public void End_Turn()
    {
        if (now_turn < 4)
        {
            now_turn = now_turn + 1;
            // if( 세팅라운드면 세팅라운드, 아니면 일반 턴)
            Setting_Round();
            // Normal_Round();
        }
        else
        {
            now_turn = 1;
            Setting_Round();
        }
    }




    /// <summary>(서버통신)
    /// 서버로 NextTurn 콜 메세지를 보냄. 자신 턴 종료 함.
    /// <param name=""
    /// </summary>
    void EndTurn()
    {
        //NextTurn(My_Turn);
        // 서버로 자신의 턴 전송
    }





    /////////////////////////////////////////////////////////////////////////
    ////////////////////////애니메이션 컨트롤////////////////////////////
    /////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// drop_num()
    ///  타일 드랍 애니메이션 함수(전체 / 로컬) 
    /// </summary>
    int drop_num = 0;
    public void Tile_Drop()
    {
        if (drop_num > 17)
        {
            Invoke("OceanDrop", 4.0f);
            Invoke("CoinAppear", 5.0f);
            CancelInvoke("Tile_Drop");
        }
        Map_List[drop_num].SetActive(true);
        Map_List[drop_num].GetComponent<Tile_Script>().tile.GetComponent<Tile_Animation>().tile_ani.SetTrigger("TileDrop");
        drop_num++;
    }

    /// <summary>
    /// OceanDrop()
    /// 바다 타일 드랍 애니메이션 호출함수(전체/로컬)
    /// </summary>
    public void OceanDrop()
    {
      //  foreach (var i in OceanList)
        //{
        //    i.SetActive(true);
        //    i.GetComponent<Tile_Script>().tile.GetComponent<Tile_Animation>().tile_ani.SetTrigger("TileDrop");
        //}

        foreach(var i in OceanList)
        {
            i.SetActive(true);
        }

    }

    /// <summary>
    /// CoinAppear()
    /// 숫자 토큰 SetActive애니메이션용 함수(전체/로컬)
    /// </summary>
    public void CoinAppear()
    {
        foreach (var i in Map_List)
        {
            if (i.GetComponent<Tile_Script>().tile_number == 7)
            {
               // print("sba13616");
                i.transform.Find("Realtoken").gameObject.SetActive(false);
                //                i.transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                i.transform.Find("Realtoken").gameObject.SetActive(true);
            }
            //i.transform.GetChild(1).gameObject.SetActive(true);
        }
    }


    //////////////////////////////////////////////////////////////////////////////////
    ////////////// UI용 함수들/////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////////

    public void OpenMenu_Toggle()
    {
        if (isMenuToggleOn == true)
        {
            MenuBtn.SetActive(false);
            isMenuToggleOn = false;
        }
        else
        {
            MenuBtn.SetActive(true);
            isMenuToggleOn = true;
        }
    }

    public void Resume_Btn()
    {
        MenuBtn.SetActive(false);
        isMenuToggleOn = false;
    }

    public void Exit_Btn()
    {
        Application.Quit();
    }

    public void BottomUI_Toggle()
    {
        if (isBottomUI_On == false)
        {
            Bottom_UIBar.transform.localPosition = new Vector3(0, -475, 0);
            Bottom_UIBtn.transform.localPosition = new Vector3(0, -405, 0);
            Bottom_UIBtn.transform.localScale = new Vector3(1, -1, 1);
            isBottomUI_On = true;
        }

        else
        {
            Bottom_UIBar.transform.localPosition = new Vector3(0, -590, 0);
            Bottom_UIBtn.transform.localPosition = new Vector3(0, -520, 0);
            Bottom_UIBtn.transform.localScale = new Vector3(1, 1, 1);
            isBottomUI_On = false;
        }

    }


    //////////////////////////////////////////////////////////////////////////////
    //////////////////////////서버 통신/////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////
    /*
    public void JoinServer()
    {
        //Thread.Sleep(5000);
        //server.Close();
        m_client = new MyTCPClient("127.0.0.1", 8080);

        JsonTest testman = new JsonTest();
        testman.id = "임시 사용자";
        int c = 1;

        while (true)
        {
            if (m_client.IsConnected)
            {
                m_client.SendMsg("접속");
                print("접속 성공");
                
                m_client.SendMsg(c+JsonUtility.ToJson(testman));                

                isConnected = true;
                break;
            }
        }
    }
    */


    private void OnDestroy()
    {
        if (isConnected == true)
        {
            //m_client.Release();
        }
        isConnected = false;
    }


    JsonClass send_tile_info()
    {
        var json = new JsonClass();
        json.function = "Start";     
        int k = 1;

        foreach (var i in Map_List)
        {
            json.Map_Info += Convert.ToString(i.GetComponent<Tile_Script>().tile_number) + " ";
            json.Map_Info += Convert.ToString(i.GetComponent<Tile_Script>().tile_property) + " ";          
            k++;
        }
        return json;
    }

    public void print_tileInfo()
    {
        int j = 1;
        foreach(var i  in Map_List)
        {
           print(" tile_info : " + j + " tile_number : " + i.GetComponent<Tile_Script>().tile_number + " tile_property : " + i.GetComponent<Tile_Script>().tile_property);           
            j++;
        }
    }
}