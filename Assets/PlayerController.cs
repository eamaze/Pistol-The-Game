using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private CharacterController characterController;
    private float verticalVelocity = 0f;
    private bool jumpCd = false;


    [SerializeField]
    public Transform camera;

    [SerializeField]
    public float speed = 10f;
    [SerializeField]
    public float jumpPower = 1f;
    [SerializeField]
    public float turnSens = 1f;
    [SerializeField]
    public float gravityValue = -9.81f;
    [SerializeField]
    public float jumpCooldownTime = 1.5f;


    public void Awake()
    {
        playerInput  = GetComponent<PlayerInput>();
        characterController = GetComponent<CharacterController>();
    }


    public void FixedUpdate()
    {
        Vector2 moveVector = playerInput.actions["Move"].ReadValue<Vector2>() * speed;



        if (playerInput.actions["Jump"].IsPressed() && characterController.isGrounded && !jumpCd)
        {
            jumpCd = true;
            verticalVelocity += Mathf.Sqrt(jumpPower * -3f * gravityValue);
            resetJumpCooldown();
            Debug.Log("test");
            jumpCd = false; //TODO
        }

        verticalVelocity += gravityValue * Time.deltaTime;

        Vector3 moveVelocity = (moveVector.x * camera.right +  moveVector.y * camera.forward);
        moveVelocity.y = verticalVelocity;

        characterController.Move(moveVelocity);


    }
    IEnumerator resetJumpCooldown()
    {
        yield return new WaitForSeconds(jumpCooldownTime);
    }


}
