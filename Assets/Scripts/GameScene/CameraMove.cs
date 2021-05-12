using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMove : MonoBehaviour {


    bool istopview = false;

	// Use this for initialization
	void Start () {
		
	}
	
    
    public void AdjustCameraView()
    {
        if(istopview == false)
        {
            transform.position+=new Vector3(0, 4, 11);
            transform.Rotate(new Vector3(25, 0, 0));

            istopview = true;
        }
        else
        {
            transform.position += new Vector3(0, -4, -11);
            transform.Rotate(new Vector3(-25, 0, 0));
            istopview = false;
        }
    }


    /*
    [SerializeField]
    float cameraMove_speed = 10.0f;



    Vector3 movePos;
    Vector2 prePos = Vector2.zero;
    Touch tempTouch;

    float prevDistance = 0.0f;

    /*
    void Update()
    {

        if (Input.touchCount > 0)
        {
            tempTouch = Input.GetTouch(0);

            if (tempTouch.phase == TouchPhase.Began) // 터치 시작
            {
            }
            else if (tempTouch.phase == TouchPhase.Moved) // 터치 이동
            {
                OnDrag();
            }
            else if (tempTouch.phase == TouchPhase.Ended) // 터치 종료
            {
                if (EventSystem.current.IsPointerOverGameObject() == false)
                    ExitDrag();
            }
        }
    }

    void OnDrag() // 드래그함수
    {
        int touchCount = Input.touchCount;

        if (touchCount == 1) // 터치 1일경우 ( 이동 )
        {
            if (EventSystem.current.IsPointerOverGameObject(0) == true) // UI 터치 제외 
            {
                return;
            }
            if (prePos == Vector2.zero)
            {
                prePos = Input.GetTouch(0).position;
                return;
            }

            Vector2 dir = (Input.GetTouch(0).position - prePos).normalized; // 방향벡터

            if ((transform.position.x < -3 && dir.x > 0) || (transform.position.x > 3 && dir.x < 0) ||
                (transform.position.y < -3 && dir.y > 0) || (transform.position.y > 3 && dir.y < 0)) // 이동 범위 조건
            {
                return;
            }
            Vector3 vec = new Vector3(dir.x, dir.y, 0);
            transform.position -= vec * cameraMove_speed * Time.deltaTime;
            prePos = Input.GetTouch(0).position;
        }


        else if (touchCount == 2) // 터치 2일경우 ( 줌인아웃 )
        {
            for (int i = 0; i < 2; i++)
            {
                if (EventSystem.current.IsPointerOverGameObject(i) == true) // UI터치 제외
                {
                    return;
                }
            }
            if (prevDistance == 0)
            {
                prevDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
                return;
            }

            float curDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
            float move = prevDistance - curDistance;

            Vector3 pos = transform.position;
            if (move < 0)
            {
                if (GetComponent<Camera>().orthographicSize < 3)
                {
                    return;
                }
                GetComponent<Camera>().orthographicSize -= cameraMove_speed * Time.deltaTime;
            }

            else if (move > 0)
            {
                if (GetComponent<Camera>().orthographicSize > 7)
                {
                    return;
                }
                GetComponent<Camera>().orthographicSize += cameraMove_speed * Time.deltaTime;
            }

            transform.position = pos;
            prevDistance = curDistance;
        }
    }

    public void ExitDrag()
    {
        prePos = Vector2.zero;
        prevDistance = 0f;
    }

    */




}
