using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyWithParticle : MonoBehaviour
{
    // ������Ʈ ���� ��ƼŬ
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

        // ������Ʈ ���� �������� ��ƼŬ ����
        //Instantiate(explosion, pos, Quaternion.identity);
        // ��ƼŬ ������Ʈ ���� ��, �������� �ʴ´ٸ� �̷���
        Transform parti = Instantiate(explosion, pos, Quaternion.identity);
        // Delay(����)�� �༭ ������Ʈ ����(destroy)
        Destroy(parti.gameObject, 2);

        //Destroy(gameObject);
        // ������Ʈ ���� ��ſ�, ������Ʈ�� ȭ�鿡 �Ⱥ��̰�
        gameObject.SetActive(false);

        // ������ ȹ�� ��(score)�� �÷���
        GameManager.AddResource();
    }
}
