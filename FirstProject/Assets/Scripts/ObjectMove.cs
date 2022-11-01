using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    // 이동속도
    float moveSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 현재 프레임에서 이동할 거리
        float moveAmount = moveSpeed * Time.deltaTime;

        // 오브젝트를 전방으로 이동
        // (1) Position 이동
        //transform.position += new Vector3(0, 0, 1);
        // (2) Translate() 함수 이동
        // 현재 위치에서 지정한 방향과 거리만큼 오브젝트를 이동시키는 함수
        //transform.Translate(Vector3.forward * moveAmount);

        // 키보드 입력
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump, Space bar is pressed..");
        }

        // 키보드로부터 오브젝트 이동
        // step1. 키보드로부터 입력 신호를 받아옴
        float vert = Input.GetAxis("Vertical");
        float horz = Input.GetAxis("Horizontal");
        // step2. 입력 값 만큼 오브젝트 이동
        transform.Translate(new Vector3(horz, 0, vert) * moveAmount);
    }
}
