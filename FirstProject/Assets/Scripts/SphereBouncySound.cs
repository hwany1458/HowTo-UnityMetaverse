using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereBouncySound : MonoBehaviour
{
    //AudioSource를 저장할 변수 선언
    AudioSource ballAudio;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Hello, world..");

        // 시작할 때 컴포넌트 열기
        // 씬이 로딩되면, AudioSource를 변수로 읽어들임
        ballAudio = GetComponent<AudioSource>();
        Debug.Log("오디오파일 = " + ballAudio.clip.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 오브젝트 충돌이 발생하면, 호출되는 이벤트 함수
    // 충돌의 상대편 정보가 매개변수 collision으로 전달됨
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("공과 충돌한 객체 = " + collision.gameObject.name);

        // AudioSource에 할당된 오디오 클립을 재생
        ballAudio.Play();
    }
}
