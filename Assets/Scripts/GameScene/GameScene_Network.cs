using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using WebSocketSharp;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using WebSocketSharp.Net;
using System.Net;
using System.Net.Sockets;
//using WebSocket4Net;
//using WebSocket4Net.Protocol;
using WebSocketSharp.Net.WebSockets;
using WebSocketSharp.Net.Security;



public class GameScene_Network : MonoBehaviour {
    

    string m_room_key;
    public bool isMaster = false;
    bool isFirstMessage = false;

    [SerializeField]
    Button Create_Btn;
    [SerializeField]
    Button Join_Btn;

    WebSocket ws;    
    public void SetupServer()
    {
        isMaster = true;
        //ws = new WebSocket("ws://localhost:8080/create");
        ws = new WebSocket(url: "ws://localhost:8080/create?japtan-room-key=13b5057f-d391-424c-8305-4d93bc61457a&japtan-user-key=user1");
                              //ws = new WebSocket(url:"ws://localhost:8080/ws");
                              //ws = new WebSocket(url:"ws://japtan.appspot.com/ws");

     //  ws.OnOpen += OnOpenHandler;
       ws.OnMessage += OnMessageHandler;
       ws.OnOpen += OnOpenHandler;
       ws.OnError += OnErrorHandler;
       ws.Connect();
               
        if (ws.IsConnected== true)
        {
            print("접속 성공");
            Create_Btn.gameObject.SetActive(false);
            Join_Btn.gameObject.SetActive(false);
        }
        else
        {
            print("접속 실패");
        }
    }

    public void JoinRoom()
    {


        if (isMaster == false)
        {
           // ws = new WebSocket("ws://localhost:8080/ws");
           
            ws = new WebSocket("ws://localhost:8080/join?japtan-room-key=13b5057f-d391-424c-8305-4d93bc61457a&japtan-user-key=user2");
            ws.OnOpen += OnOpenHandler;
            ws.OnMessage += OnMessageHandler;
            ws.OnOpen += OnOpenHandler;
            ws.OnError += OnErrorHandler;
            ws.Connect();            

            if (ws.IsConnected == true)
            {
                print("접속 성공");
                Create_Btn.gameObject.SetActive(false);
                Join_Btn.gameObject.SetActive(false);
            }
            else
            {
                print("접속 실패");
            }
        }
    }    

    public void OnDestroy()
    {
        if (ws!=null)
        {
            ws.Close();
        }
        print("끝");
    }

    public void Sendmsg(JsonClass _msg)
    {
        ws.Send(JsonUtility.ToJson(_msg));
    }
   
   
   private void OnErrorHandler(object sender, ErrorEventArgs e)
   {
       Debug.Log(e.Message);
   }

    [SerializeField]
    Text ui_count;
    int k = 0;

    bool firstmsg = false;
    //이 부분을 check 코루틴이 끝날때 실행해서 대기하는 것으로 변경하면 딜레이 없어짐.
 private void OnMessageHandler(object sender, MessageEventArgs e)
 {
        MessageQueue.GetInstance.PushData(e.Data);

      // if (firstmsg == false)
      // {
      //     firstmsg = true;
      //     MessageQueue.GetInstance.PushData(e.Data);
      //    // print(e.data);
      // }
      // else
      // {
      //     firstmsg = false;
      // }

     //Game_Director.SendMessage("Receivemsg", e.Data);
     //Game_Director.GetComponent<GameScene_Director>().Receivemsg(e.Data);
     //Debug.Log(e.Message);
 }
 
    private void OnOpenHandler(object sender, EventArgs e)
    {
        Debug.Log("socket conencted");
    }

    
}