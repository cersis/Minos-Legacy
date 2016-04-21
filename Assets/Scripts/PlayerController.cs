using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{

    public float speed = 6.0F;
    public float walkSlowerRate = 0.5F;
    public float rotationSpeed = 5.0F;
    public float jumpSpeed = 5.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private new Animation animation;
    public AnimationClip defaultAnimation;
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        animation = GetComponent<Animation>();
    }
    void Update()
    {
        if (controller.isGrounded)
        {
            Move();
        }
        Attack();
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    private void Attack()
    {

        if (Input.GetMouseButtonDown(0))
        {
            animation.Play("attack");
        }
    }

    private void ChooseAnimation(String speed)
    {

        if (moveDirection.y == 0)
        {
            if (moveDirection != Vector3.zero)
                animation.Play(speed);
            else
                animation.Play("idle");

        }

    }

    private void Move()
    {
        Forward();
        Rotate();
        Jump();

    }

    private void Jump()
    {
        if (Input.GetButton("Jump"))
        {
            moveDirection.y = jumpSpeed;
            animation.Play("jump");
        }
    }

    private void Rotate()
    {
        Vector3 rotateDirection = new Vector3(0, Input.GetAxis("Horizontal"), 0);
        rotateDirection *= rotationSpeed;
        transform.Rotate(rotateDirection, Space.Self);
    }

    private void Forward()
    {
        moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;
        ChoseAction();
    }
    /// <summary>
    /// Bieg, ruch, czy atak
    /// </summary>
    private void ChoseAction()
    {
        if (!animation.IsPlaying("attack"))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveDirection *= walkSlowerRate;
                ChooseAnimation("walk");
            }
            else
            {
                ChooseAnimation("run");
            }
        }
        else
        {
            moveDirection = Vector3.zero;
        }
    }
}
