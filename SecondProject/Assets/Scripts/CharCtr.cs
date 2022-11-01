using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class CharCtr : MonoBehaviour
{
    [SerializeField]
    private Transform characterBody;
    [SerializeField]
    private Transform cameraArm;
    [SerializeField]
    private GameObject local_Camera;
    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;

    public PhotonView PV;

    public Animator animator;

    public bool isJump = false;

    private void Awake()
    {
        if (PV.IsMine)
        {
            CharCtr.LocalPlayerInstance = this.gameObject;
            local_Camera.SetActive(true);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
        else
        {
            LookAround();
            Move();
        }
        if (Input.GetKeyDown(KeyCode.Space) && !isJump)
        {
            Jump();
        }
    }
   
    private void Move()
    {
        // �̵� ���� ���ϱ� 1
        //Debug.DrawRay(cameraArm.position, cameraArm.forward, Color.red);

        // �̵� ���� ���ϱ� 2
        //Debug.DrawRay(cameraArm.position, new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized, Color.red);

        // �̵� ����Ű �Է� �� ��������
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        // �̵� ����Ű �Է� ���� : �̵� ���� ���Ͱ� 0���� ũ�� �Է��� �߻��ϰ� �ִ� ��
        bool isMove = moveInput.magnitude != 0;
        // �Է��� �߻��ϴ� ���̶�� �̵� �ִϸ��̼� ���
        animator.SetBool("isMove", isMove);
        if (isMove)
        {
            // ī�޶� �ٶ󺸴� ����
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            // ī�޶��� ������ ����
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            // �̵� ����
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            // �̵��� �� ī�޶� ���� ���� �ٶ󺸱�
            //characterBody.forward = lookForward;
            // �̵��� �� �̵� ���� �ٶ󺸱�
            characterBody.forward = moveDir;
            // �̵�
            transform.position += moveDir * Time.deltaTime * 5f;
        }
    }

    private void LookAround()
    {
        // ���콺 �̵� �� ����
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        // ī�޶��� ���� ������ ���Ϸ� ������ ����
        Vector3 camAngle = cameraArm.rotation.eulerAngles;
        // ī�޶��� ��ġ �� ���
        float x = camAngle.x - mouseDelta.y;

        // ī�޶� ��ġ ���� �������� 70�� �Ʒ������� 25�� �̻� �������� ���ϰ� ����
        if (x < 180f)
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }

        // ī�޶� �� ȸ�� ��Ű��
        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }
    private void Jump()
    {
        isJump = true;
        Rigidbody rigid = gameObject.GetComponent<Rigidbody>();
        rigid.AddForce(Vector3.up * 5, ForceMode.Impulse);

    }
    private void OnCollisionEnter(Collision collision)
    {
        isJump = false;
    }
}
