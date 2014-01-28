using UnityEngine;
using System.Collections;

[@RequireComponent(typeof(CharacterController))]
public class ControlPlayer : MonoBehaviour
{
    //�������� ����, �������� �������, ����������� ������� �������, ������ ������ ��� �����, �� ��������� ������ �� �����
    public float moveSpeed = 7f, jumpSpeed = 0.1f, jumpHeight = 2, camHeight = 3f, camDistance = 7f;

    private bool isJumping = false;
    private Vector3 lastPosition;
    private CharacterController cc;

    void Jump()
    {
        if (cc.isGrounded) //���� �� ����� �� ����
            isJumping = false; //�� �� �� ������ ��������
        if (Input.GetKeyDown(KeyCode.Space) && cc.isGrounded) //���� �� ����� �� ���� � ������ �� ������
        {
            //�� �������� ������� ������� ������, � �����������, �� �� ���� ����� ��������
            isJumping = true;
            lastPosition = transform.position;
        }
        if (isJumping) //���� �� ������
        {
            if (transform.position.y < jumpHeight + lastPosition.y) //���� �� �� ����������� �� ������� ��� 
                cc.Move(transform.up * jumpSpeed); //���������� �������� ���� ������� ������
            else //��-������ ��������� �������� ���� ������� �����
                isJumping = false; //���������� ��������, � ������� ���������
        }
    }

    void Start()
    {
        //��������� ��������� CharacterController, ��� ������� ���� �� ��������� �����
        cc = GetComponent<CharacterController>();

        //������ ������ ��������� �� ������, �� �������� ������� ������� ������
        Camera.allCameras[0].transform.parent = transform;
        Camera.allCameras[0].transform.position = transform.position + new Vector3(0, camHeight, -camDistance);
    }
    void Update()
    {
        //��������� ����� �� 90 �������(����, ��� ������)
        if (Input.GetKeyDown(KeyCode.D))
            transform.Rotate(new Vector3(0, 90, 0));
        if (Input.GetKeyDown(KeyCode.A))
            transform.Rotate(new Vector3(0, -90, 0));

        //������ ����� ������� ������, �������� �� ��������
        cc.SimpleMove(transform.forward * moveSpeed);

        //��������� ������� �������
        Jump();
    }
}
