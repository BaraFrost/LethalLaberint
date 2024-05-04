using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //�������� ���������
    public float speedMove;//�������� ���������
    public float jumpPower;//���� �����
    public float rotationSpeed; //�������� ��������

    //��������� �������� ��� ���������
    private float gravityForce;//���������� ���������
    private Vector3 moveVector;//����������� �������� ���������

    //������ �� ����������
    private CharacterController ch_controller;
    private Animator ch_animator;
    

    private void Start()
    {
        ch_animator = GetComponent<Animator>();
        ch_controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        CharacterMove();
        GamingGravity();
    }
     

    //����� ����������� ���������
    private void CharacterMove()
    {   //����������� �� ����������� 
        if (ch_controller.isGrounded)
        {
            moveVector = Vector3.zero;
            moveVector.x = 0;//Input.GetAxis("Horizontal") * speedMove;
            moveVector.z = Input.GetAxis("Vertical") * speedMove;
            if(gravityForce< 0) 
            {
                ch_animator.SetBool("Jump", false);
            }
            


            //�������� ������������ ���������
            if (moveVector.x != 0 || moveVector.z != 0) ch_animator.SetBool("Move", true);
            else ch_animator.SetBool("Move", false);
            //������� ��������� � ������� ����������� �����������
            if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
            {
                Vector3 direct = Vector3.RotateTowards(transform.forward, moveVector, speedMove, 0.0f);
                // Quaternion.LookRotation(direct);
            }
            transform.rotation = Quaternion.Euler( Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime * Vector3.up + transform.rotation.eulerAngles);
        }
        moveVector.y = gravityForce;
        ch_controller.Move(Quaternion.LookRotation( ch_controller.transform.forward) * moveVector * Time.deltaTime);//����� ������������ �� ������������
    }

    //����� ����������
    private void GamingGravity()
    {
        if (!ch_controller.isGrounded) gravityForce -= 20f * Time.deltaTime;
        else gravityForce = -1f;
        if (Input.GetKeyDown(KeyCode.Space) && ch_controller.isGrounded)
        {
            gravityForce = jumpPower;
            ch_animator.SetBool("Jump",true);
        }
    }
}
