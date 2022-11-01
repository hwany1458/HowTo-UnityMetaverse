using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoveAndCollide : MonoBehaviour
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

    // 충돌 처리
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter event is occurred.." + other.gameObject.tag);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter event is occurred..." + collision.gameObject.tag);

        // 충돌이 발생한 오브젝트가 아이템이면 
        if (collision.gameObject.tag == "Item")
        {
            // (1) 오브젝트 제거
            //Destroy(collision.gameObject);
            // (2) 오브젝트를 화면에서 안보이게
            //collision.gameObject.SetActive(false);
            // (3) 오브젝트에서는 충돌 판정만 하고,
            // 충돌에 따른 세부처리는 충돌 대상 오브젝트에서 처리하는 것이 효율적임.
            // (충돌 대상 오브젝트는 collision.gameObject)
            collision.gameObject.SendMessage("DestroySelf", transform.position);
        }
    }
}
