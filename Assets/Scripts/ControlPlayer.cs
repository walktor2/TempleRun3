using UnityEngine;
using System.Collections;

[@RequireComponent(typeof(CharacterController))]
public class ControlPlayer : MonoBehaviour
{
    //швидкість руху, швидкість стрибка, максимальна границя стрибка, висота камери над героєм, та дистанція камера до героя
    public float moveSpeed = 7f, jumpSpeed = 0.1f, jumpHeight = 2, camHeight = 3f, camDistance = 7f;

    private bool isJumping = false;
    private Vector3 lastPosition;
    private CharacterController cc;

    void Jump()
    {
        if (cc.isGrounded) //якщо ми стоїмо на землі
            isJumping = false; //то ми не можемо стрибати
        if (Input.GetKeyDown(KeyCode.Space) && cc.isGrounded) //якщо ми стоїмо на землі і нажали на ПРОБІЛ
        {
            //то записуємо останню позицію гравця, і повідомляємо, що він буде зараз стрибати
            isJumping = true;
            lastPosition = transform.position;
        }
        if (isJumping) //якщо він стрибає
        {
            if (transform.position.y < jumpHeight + lastPosition.y) //якщо ми не дострибнули до верхньої межі 
                cc.Move(transform.up * jumpSpeed); //продовжуємо надавати силу стрибка гравцю
            else //по-іншому перестаємо надавати силу стрибка герою
                isJumping = false; //припиняємо стрибати, і задіюємо гравітацію
        }
    }

    void Start()
    {
        //знаходимо компонент CharacterController, для задання руху та гравітації героя
        cc = GetComponent<CharacterController>();

        //робимо камеру дочірньою до гравця, та записуємо потрібну позицію камери
        Camera.allCameras[0].transform.parent = transform;
        Camera.allCameras[0].transform.position = transform.position + new Vector3(0, camHeight, -camDistance);
    }
    void Update()
    {
        //повертаємо героя на 90 градусів(вліво, або вправо)
        if (Input.GetKeyDown(KeyCode.D))
            transform.Rotate(new Vector3(0, 90, 0));
        if (Input.GetKeyDown(KeyCode.A))
            transform.Rotate(new Vector3(0, -90, 0));

        //рухаємо героя постійно вперед, відповідно до повороту
        cc.SimpleMove(transform.forward * moveSpeed);

        //викликаємо функцію стрибка
        Jump();
    }
}
