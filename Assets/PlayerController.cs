using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float jumpSpeed = 2f;
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] private float mouseSensitivity = 2f;

    private Vector3 moveDirection = Vector3.forward;
    private Quaternion playerRot;

    void Start()
    {
        playerRot = transform.localRotation;
    }

    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= moveSpeed;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        Look();
    }

    private void Look()
    {
        float xRot = Input.GetAxis("Mouse X") * 2f;
        playerRot *= Quaternion.Euler(0f, xRot, 0f);
        transform.localRotation = playerRot;
    }
}
