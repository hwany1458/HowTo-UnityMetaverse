using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyWithParticle : MonoBehaviour
{
    // 오브젝트 제거 파티클
    public Transform explosion;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("ObjectDestroyWithParticle");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Object Destroy
    void DestroySelf(Vector3 pos)
    {
        //Debug.Log("DestroySelf method is called...");

        // 오브젝트 제거 시점에서 파티클 실행
        //Instantiate(explosion, pos, Quaternion.identity);
        // 파티클 오브젝트 생성 후, 삭제되지 않는다면 이렇게
        Transform parti = Instantiate(explosion, pos, Quaternion.identity);
        // Delay(지연)을 줘서 오브젝트 제거(destroy)
        Destroy(parti.gameObject, 2);

        //Destroy(gameObject);
        // 오브젝트 제거 대신에, 오브젝트가 화면에 안보이게
        gameObject.SetActive(false);

        // 아이템 획득 수(score)를 올려줌
        GameManager.AddResource();
    }
}
