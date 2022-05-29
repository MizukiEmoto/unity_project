using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove : MonoBehaviour {
	
	[SerializeField]
	float speed = 5f; // 移動速度
    [SerializeField]
	float rotateSpeed = 10f; // 回転速度

	Vector3 inputVector = Vector3.zero; // 入力の方向を保存

	Rigidbody rb;
	Animator animator;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    
    void FixedUpdate()
    {
        GetInputAxis(); // 入力を取得して
        Rotate(); // 移動方向を向いて
        Move(); // 移動して
        ChangeAnimation(); // アニメーションを変更する
        GetJump();
    }

    void GetJump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("is_jumping", true);
        }
        else
            animator.SetBool("is_jumping", false);
    }

    void GetInputAxis()
	{
		inputVector.x = Input.GetAxisRaw("Horizontal");
        inputVector.z = Input.GetAxisRaw("Vertical");
	}

    void Rotate()
    {
        // 入力がなければ回転しない
        if(inputVector == Vector3.zero)
            return;
        
        var targetRotation = Quaternion.LookRotation(inputVector);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
    }

    void Move()
    {
        rb.velocity = inputVector * speed;
    }

    void ChangeAnimation()
    {
        if(inputVector.magnitude > 0)
            animator.SetBool("isRun", true);
        else
            animator.SetBool("isRun", false);
    }
}
