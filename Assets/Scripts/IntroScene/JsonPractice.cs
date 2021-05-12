using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using WebSocketSharp;
using GooglePlayGames.BasicApi;
using GooglePlayGames;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;




[System.Serializable]
public class JsonTest
{
    public string id;
  //public float f;
  //public bool b;
  //public Vector3 v;
  //public string str;
  //public int i;
   }


public class JsonReceive
{
    public string Id;
    //public string player;
   // public bool owner;
   // public string id;
}


/*
public class JsonPractice : MonoBehaviour {

    [SerializeField]
    Text m_uiText;
    

    char c ;
    WebSocket ws;
    int i = 0;
    JsonTest json;
    private void Awake()
    {
        ws = new WebSocket(url: "ws://localhost:8080/echo");
        //ws = new WebSocket(url:"ws://localhost:8080/ws");
        //ws = new WebSocket(url:"ws://japtan.appspot.com/ws");
        // ws.Connect();       ws.Connect();

        ws.OnOpen += OnOpenHandler;
        ws.OnMessage += OnMessageHandler;
        ws.OnOpen += OnOpenHandler;
        ws.OnError += OnErrorHandler;

        ws.Connect();
    }

    JsonTest _msg;
    void Start() {

        recmsg = new JsonReceive();
        _msg = new JsonTest();
        //PlayGamesClientConfiguration config = new PlayGamesClientConfiguration
        //    .Builder()
        //   
        //    .Build();
        ////PlayGamesPlatform.InitializeInstance(config);
        //PlayGamesPlatform.Activate();

        json = new JsonTest();
        json.id = "임시 사용자";

        if (ws.IsConnected == true)
        {
            print("접속 성공");
        }
        else
        {
            print("접속 실패");
        }
        // json.i = 10;
        // json.f = 6.6f;
        // json.b = true;
        // json.v = new Vector3(1, 2, 3);
        // json.str = "hi";
        // json.iArray = new int[3] { 1, 2, 3 };

        //JsonUtility.ToJson(json);
        // ws = new WebSocket(url: "ws://japtan.appspot.com/ws");
        // ws.OnMessage += OnMessageHandler;


        //     ws.OnMessage += OnMessageHandler;
        //   ws.OnClose += OnCloseHandler;
        //ws.Send(JsonUtility.ToJson(json));

        //InvokeRepeating("gojason", 1, 3.0f);


        //ws = new WebSocket(url: "ws://japtan.appspot.com/ws");
        //ws.Connect();
        //c = (char)i;
        //ws.Send(c + JsonUtility.ToJson(json));

        //Client client = new Client("ws://japtan.appspot.com/ws");
        ////client.Opened += SocketOpened;
        ////client.Message += SocketMessage;
        ////client.SocketConnectionClosed += SocketConnectionClosed;
        ////client.Error += SocketError;
        ////
        //client.Connect();
        //
        //// private void SocketOpened(object sender, MessageEventArgs e)
        //// {
        ////     //invoke when socket opened
        //// }
        //client.Send(JsonUtility.ToJson(json));
        //
    }
    public void gojason()
    {
      ////ws = new WebSocket(url: "ws://japtan.appspot.com/ws");
      //ws = new WebSocket(url: "ws://localhost8080/ws");
      //ws.Connect();
      //ws.Send(c + JsonUtility.ToJson(json));        
      //c++;
    }

    
    PlayGamesPlatform instance;

    public void Login()
    {


        if (Social.localUser.authenticated == false)
        {

            Social.localUser.Authenticate((_isSuccess) =>
            {
                if (_isSuccess == true)
                {
                    //PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_first_login, 100, null);

                    m_uiText.text = "로그인 성공";
                    //PlayerInfo.Player_ID = Social.localUser.id;
                    //reportprogress();
                    //m_text.text = "로그인 성공";
                }
                else
                {
                    m_uiText.text = "로그인 실패";
                    //m_text.text = "로그인 실패";
                }
            });

            //login();
        }
        else
        {
            //((PlayGamesPlatform)Social.Active).SignOut();
            PlayGamesPlatform pp = (PlayGamesPlatform)Social.Active;
            pp.SignOut();
            //PlayerInfo.Player_ID = "임시 사용자";
            // m_text.text = "로그아웃";
        }


        //  instance = PlayGamesPlatform.Instance;
        //
        //  if (instance == null)
        //      return;
        //
        //  if (instance.IsAuthenticated() == false)
        //  {
        //      instance.Authenticate(isSuccess =>
        //      {
        //          if (isSuccess)
        //          {
        //              PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_first_login, 100, null);
        //              m_uiText.text = "로그인 성공";
        //          }
        //
        //          else
        //              m_uiText.text = "로그인 실패";
        //      });
        //  }
        //  else
        //  {
        //      instance.SignOut();
        //      m_uiText.text = "로그 아웃";
        //  }
    }

    public void SendInfo()
    {        
        c = '1';
        ws.Send(JsonUtility.ToJson(json));
        Debug.Log(JsonUtility.ToJson(json));
    }

    

    public void ShowAchievement()
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated())
            PlayGamesPlatform.Instance.ShowAchievementsUI();
    }

    string msg;

    private void OnCloseHandler(object sender, CloseEventArgs e)
    {
        Debug.Log(e.Code);
    }

    JsonReceive recmsg;
    string str_;

    private void OnMessageHandler(object sender, MessageEventArgs e)
    {
        print("shiba");

        //str_ = e.Data.ToString();
        // recmsg = JsonUtility.FromJson<JsonReceive>(Convert.ToString(e.RawData));
        var recmsg1 = JsonUtility.FromJson<JsonTest>(Convert.ToString(e.Data));
       // print(recmsg1.roomid);
        print("받은 룸 아이디 : " + recmsg1.id);
        //JsonUtility.FromJsonOverwrite(e.RawData.ToString(), recmsg);

       // _msg.id = Convert.ToString(e.RawData);
       //_msg = JsonUtility.FromJson<JsonTest>(Convert.ToString(e.RawData));
       //string _msg = Convert.ToString(e.RawData);
       //  for (int i = 0; i<e.RawData.Length;i++)
       //  {
       //      Debug.Log((char)e.RawData[i]);
       //  }
       //  //Debug.Log(_msg.id);
        //Debug.Log(recmsg1);
    }
    

    
    private void OnErrorHandler(object sender, ErrorEventArgs e)
    {
        Debug.Log(e.Message);
    }

    private void OnOpenHandler(object sender, EventArgs e)
    {
        Debug.Log("websocket Connected");
    }


    private void OnDestroy()
    {
       // ws.Close();
    }


    // Update is called once per frame
    void Update()
    {

        //      msg = "";

        //JsonUtility.FromJson<new string msg()>;

    }



}
*/