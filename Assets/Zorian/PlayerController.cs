using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Добавляем пространство имен UnityEngine.UI для работы с UI

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;    // Скорость движения игрока
    public float lookSpeed = 2f;    // Скорость вращения игрока
    public float jumpForce = 5f;    // Сила прыжка
    public Camera playerCamera;     // Камера игрока
    public Image crosshair;         // Прицел

    private Rigidbody rb;
    private float rotationX = 0f;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Заморозить вращение Rigidbody для стабильности

        if (playerCamera == null)
        {
            playerCamera = Camera.main;  // Автоматическое назначение главной камеры
        }

        Cursor.lockState = CursorLockMode.Locked;  // Блокировка курсора в центре экрана

        // Скрыть курсор мыши
        Cursor.visible = false;
    }

    void Update()
    {
        // Движение по клавишам WASD
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 movement = transform.right * moveX + transform.forward * moveZ;
        transform.position += movement * moveSpeed * Time.deltaTime;

        // Повороты по осям мыши
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);  // Ограничение угла наклона камеры

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // Проверка, на земле ли игрок
        isGrounded = rb.velocity.y == 0;

        // Прыжок
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
