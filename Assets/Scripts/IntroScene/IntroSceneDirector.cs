using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class IntroSceneDirector : MonoBehaviour {

       
    bool Login() // 리턴 값 로그인 성공실패. 서버접속    
    {
        return true;
    }


    public void NextScene()
    {

        SceneManager.LoadScene("GameScene");
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    
}
