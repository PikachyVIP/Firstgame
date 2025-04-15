using UnityEngine;

public class Controller : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float gravity = -9.81f;
    public float jumpHeight = 1f;
    public float ceilingCheckDistance = 16f;

    [Header("Controls")]
    public KeyCode runKey = KeyCode.LeftShift;

    [Header("Ceiling Check")]
    public GameObject ceilingSensor; // Перетащите сюда CeilingSensor из редактора

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float currentSpeed;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentSpeed = walkSpeed;
    }

    void Update()
    {
        // Проверка на землю
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Переключение бега/ходьбы
        currentSpeed = Input.GetKey(runKey) ? runSpeed : walkSpeed;

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

        if ((controller.collisionFlags & CollisionFlags.Above) != 0)
        {
            velocity.y = Mathf.Min(-1f, velocity.y); // Гарантируем, что скорость не положительная
        }
        

     

        

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }



}