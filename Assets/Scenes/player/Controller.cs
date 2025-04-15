using UnityEngine;

public class Controller : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;      // Обычная скорость
    public float runSpeed = 10f;      // Скорость бега
    public float gravity = -9.81f;    // Гравитация
    public float jumpHeight = 1f;     // Высота прыжка1

    [Header("Controls")]
    public KeyCode runKey = KeyCode.LeftShift; // Клавиша бега

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float currentSpeed;       // Текущая скорость (меняется при беге)

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentSpeed = walkSpeed;     // Начинаем с обычной скорости
    }

    void Update()
    {
        // Проверка на землю
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Фикс "дрожания" на земле
        }

        // Переключение между ходьбой и бегом (удерживайте Shift)
        if (Input.GetKeyDown(runKey))
        {
            currentSpeed = runSpeed;
        }
        if (Input.GetKeyUp(runKey))
        {
            currentSpeed = walkSpeed;
        }

        // Движение WASD
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 moveDir = transform.forward * ver + transform.right * hor;
        controller.Move(moveDir * currentSpeed * Time.deltaTime);

        // Прыжок
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Гравитация
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}