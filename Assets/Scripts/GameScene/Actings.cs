using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actings : MonoBehaviour {
    
    //////////////////////////////////////////////////////////////////////////////////////////////
    ///////////서버 통신용 함수들////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////
       
    /// <summary> (서버 통신)
    /// ReCeivedMap_List()
    ///  방장이 로컬에서 생성한 맵리스트를 서버로 전송하면 서버에서 방 전체에게 전달하는 함수.
    /// </summary>
    /// <param name="_Map_List" 방장이 서버로 보낸 맵 리스트.(자료형은 상의)
    /// 방장이 아닌 유저들은 이 정보를 자신의 로컬 맵 리스트에 적용.></param>
    /// <param name="_orderNumber" 게임 진행 순서
    /// 
    /// 서버 할일 : 1. 이 함수 콜과 함께 들어온 인자들을 전체 사용자들에게 뿌림
    ///                2. 방 인원에 맞게 1~4 숫자 랜덤으로 섞어서 각각 하나씩 줌(순서)  ></param>   
    void ReCeivedMap_List(List<GameObject> _Map_List, int _orderNumber)
    {
        // 현재 내부 실행확인을 위해 로컬에서 순서 정해서 돌리고 있음.

        // GameDatass.My_TurnNumber = _orderNumber;

        // if(Not_Master)
        // {
        //  Map_List = _Map_List;        
        // }

        // 자신이 1번순서이면 settingRound 시작
        // if(My_Turn==1)
        // {
        //     Setting_Round();
        // }
    }



    /// <summary> (서버 통신)
    /// 서버로 부터 시간을 가져오는 함수. 타이머에 사용
    /// <param name=""
    /// 서버 할일 :  1. 이 함수 콜을 받으면 현재 서버 시간을 요청자에게 보냄.
    ///                    클라에서는 1초에 한번씩 이 함수를 실행시켜 로컬값과 검증하여 적용  
    /// </summary> 
    float GetTimeFromServer(float _ServerTime)
    {
        return _ServerTime;
    }



    /// <summary>(서버 통신)
    ///  서버가 보내는 콜 함수. 인자와 자신의 턴을 비교하여 자신의 턴이면 턴 시작 함수 실행.
    ///  <param name=""
    ///  서버 할일 :  1. 이 함수 콜과 함께 들어온 인자들을 전체 사용자들에게 뿌림
    /// </summary>
    void NextTurn(int _LastTurn)
    {
       // if (_LastTurn == 4)
       // {
       //     if (My_Turn == 1)
       //     {
       //         // 라운드 실행
       //     }
       //     else
       //         return;
       // }

      //  else
      //  {
      //      if (_LastTurn + 1 == My_Turn)
      //      {
      //          // 라운드 실행
      //      }
      //      else
      //          return;
       // }

    }

    /// <summary>
    /// Build_Town()
    /// 건물 짓기 콜
    /// </summary>
    /// <param name="_buildingPoint" : 건물 지을 위치를 가진 배열></param>
    /// <param name="_playerOrder" : 건물 지은 플레이어
    /// 
    /// 서버 할일 : 이 함수 콜과 함께 들어온 인자들을 전체 사용자들에게 뿌림</param>
    void Build_Town(int[] _buildingPoint, int _OwnerPlayer)
    {
        if (_buildingPoint[2] == 1)
        {
         //   var newTown = Map_List[_buildingPoint[0]].GetComponent<Tile_Script>().nearBillage[_buildingPoint[1]];
         //   newTown.GetComponent<BuildingPoint_Script>().Town.SetActive(true);
         //   newTown.GetComponent<BuildingPoint_Script>().OwnerPlayer = _OwnerPlayer;
         //   newTown.GetComponent<BuildingPoint_Script>().isTownOn = true;
        }//
        if (_buildingPoint[2] == 2)
        {
          //  var newCastle = Map_List[_buildingPoint[0]].GetComponent<Tile_Script>().nearBillage[_buildingPoint[1]];
         //   newCastle.GetComponent<BuildingPoint_Script>().Castle.SetActive(true);
         //   newCastle.GetComponent<BuildingPoint_Script>().OwnerPlayer = _OwnerPlayer;
         //   newCastle.GetComponent<BuildingPoint_Script>().isTownOn = true;
        }
    }


    /// <summary>
    /// Build_Road
    /// </summary>
    /// <param name="_OwnerPlayer"
    /// 서버 할일 : 이 함수 콜과 함께 들어온 인자들을 전체 사용자들에게 뿌림></param>
    void Build_Road(int[] _RoadPoint, int _OwnerPlayer)
    {
     //   var newRoad = Map_List[_RoadPoint[0]].GetComponent<Tile_Script>().nearRoad[_RoadPoint[1]];
     //   newRoad.GetComponent<RoadPoint_Script>().Road.SetActive(true);
     //   newRoad.GetComponent<RoadPoint_Script>().OwnerPlayer = _OwnerPlayer;
    }





    /// <summary>
    /// RollDice
    /// </summary>
    /// <param name="_diceinfo" 주사위 굴려 나온 결과값
    /// 서버 할일 : 1. 이 함수 요청이 오면 사용자 전체에게 주사위 굴린 값을 보내줌></param>
    void RollDice(int[] _diceinfo)
    {

    }


    /// <summary>
    ///  get_Resources
    ///  자원 획득하면 콜함
    /// </summary>
    /// <param name="_Resources"></param>
    /// <param name="_Number"
    /// 서버 할일 : 1. 이 함수 콜과 함께 들어온 인자들을 전체 사용자에게 전송></param>
    void get_Resources(int[] _Resources, int _OwnerPlayer)
    {


    }




}
