using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    //Основные параметры
    public float speedMove;//скорость персонажа
    public float jumpPower;//сила пыжка
    public float rotationSpeed; //скорость вращения

    //Параметры геймплея для персонажа
    private float gravityForce;//гравитация персонажа
    private Vector3 moveVector;//направление движения персонажа

    //Ссылки на компоненты
    private CharacterController ch_controller;
    private Animator ch_animator;


    private void Start() {
        ch_animator = GetComponent<Animator>();
        ch_controller = GetComponent<CharacterController>();
    }

    private void Update() {
        CharacterMove();
        GamingGravity();
    }


    //Метод перемещения персонажа
    private void CharacterMove() {   //перемещение по поверхности 
        moveVector = Vector3.zero;
        moveVector.x = Input.GetAxis("Horizontal") * speedMove;
        moveVector.z = Input.GetAxis("Vertical") * speedMove;
        if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0) {
            Vector3 direct = Vector3.RotateTowards(transform.forward, moveVector, rotationSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(direct);
        }
        if (ch_controller.isGrounded) {

            if (gravityForce < 0) {
                ch_animator.SetBool("Jump", false);
            }

            //анимация передвижения персонажа
            if (moveVector.x != 0 || moveVector.z != 0) ch_animator.SetBool("Move", true);
            else ch_animator.SetBool("Move", false);
            //поворот персонажа в сторону направления перемещения

            // transform.rotation = Quaternion.Euler( Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime * Vector3.up + transform.rotation.eulerAngles);
        }
        moveVector.y = gravityForce;
        ch_controller.Move(/*Quaternion.LookRotation( ch_controller.transform.forward) */ moveVector * Time.deltaTime);//метод передвижения по направлениям
    }

    //метод гравитации
    private void GamingGravity() {
        if (!ch_controller.isGrounded) gravityForce -= 20f * Time.deltaTime;
        else gravityForce = -1f;
        if (Input.GetKeyDown(KeyCode.Space) && ch_controller.isGrounded) {
            gravityForce = jumpPower;
            ch_animator.SetBool("Jump", true);
        }
    }
}
