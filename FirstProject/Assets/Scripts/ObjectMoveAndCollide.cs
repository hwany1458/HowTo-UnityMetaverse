using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoveAndCollide : MonoBehaviour
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

    // �浹 ó��
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter event is occurred.." + other.gameObject.tag);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter event is occurred..." + collision.gameObject.tag);

        // �浹�� �߻��� ������Ʈ�� �������̸� 
        if (collision.gameObject.tag == "Item")
        {
            // (1) ������Ʈ ����
            //Destroy(collision.gameObject);
            // (2) ������Ʈ�� ȭ�鿡�� �Ⱥ��̰�
            //collision.gameObject.SetActive(false);
            // (3) ������Ʈ������ �浹 ������ �ϰ�,
            // �浹�� ���� ����ó���� �浹 ��� ������Ʈ���� ó���ϴ� ���� ȿ������.
            // (�浹 ��� ������Ʈ�� collision.gameObject)
            collision.gameObject.SendMessage("DestroySelf", transform.position);
        }
    }
}
