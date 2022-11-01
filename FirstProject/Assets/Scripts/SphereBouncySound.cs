using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereBouncySound : MonoBehaviour
{
    //AudioSource�� ������ ���� ����
    AudioSource ballAudio;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Hello, world..");

        // ������ �� ������Ʈ ����
        // ���� �ε��Ǹ�, AudioSource�� ������ �о����
        ballAudio = GetComponent<AudioSource>();
        Debug.Log("��������� = " + ballAudio.clip.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ������Ʈ �浹�� �߻��ϸ�, ȣ��Ǵ� �̺�Ʈ �Լ�
    // �浹�� ����� ������ �Ű����� collision���� ���޵�
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("���� �浹�� ��ü = " + collision.gameObject.name);

        // AudioSource�� �Ҵ�� ����� Ŭ���� ���
        ballAudio.Play();
    }
}
