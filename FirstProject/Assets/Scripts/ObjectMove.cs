using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    // �̵��ӵ�
    float moveSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ���� �����ӿ��� �̵��� �Ÿ�
        float moveAmount = moveSpeed * Time.deltaTime;

        // ������Ʈ�� �������� �̵�
        // (1) Position �̵�
        //transform.position += new Vector3(0, 0, 1);
        // (2) Translate() �Լ� �̵�
        // ���� ��ġ���� ������ ����� �Ÿ���ŭ ������Ʈ�� �̵���Ű�� �Լ�
        //transform.Translate(Vector3.forward * moveAmount);

        // Ű���� �Է�
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump, Space bar is pressed..");
        }

        // Ű����κ��� ������Ʈ �̵�
        // step1. Ű����κ��� �Է� ��ȣ�� �޾ƿ�
        float vert = Input.GetAxis("Vertical");
        float horz = Input.GetAxis("Horizontal");
        // step2. �Է� �� ��ŭ ������Ʈ �̵�
        transform.Translate(new Vector3(horz, 0, vert) * moveAmount);
    }
}
