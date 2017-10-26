using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitychandemo1 : MonoBehaviour
{

    private Animator animator;
    private bool Active;//updateの有効化on/off
                        // Use this for initialization
    private CharacterController controller;
    private Vector3 moveDirection;



    void Start()
    {
        Active = true;
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Active == true && Input.GetKey("up"))
        {
            transform.position += transform.forward * 0.08f;
            animator.SetBool("isrunning", true);
        }
        else
        {
            animator.SetBool("isrunning", false);
        }
        if (Active == true && Input.GetKey("right"))
        {
            transform.Rotate(0, 2, 0);
        }
        if (Active == true && Input.GetKey("left"))
        {
            transform.Rotate(0, -2, 0);
        }
        //=======================================
        //ジャンプ
        //=======================================
        if (Input.GetKeyDown("space") && controller.isGrounded)
        {
            if (animator.GetBool("isrunning"))
            {
                moveDirection.y = 5; //ジャンプするベクトルの代入
            }
            else
            {
                moveDirection.y = 8;

            }
            animator.SetBool("isjump", true);

        }
        else
        {
            if (controller.isGrounded)
            { //地面についているか判定
                animator.SetBool("isjump", false);
            }
        }
        moveDirection.y -= 10 * Time.deltaTime; //重力計算
        controller.Move(moveDirection * Time.deltaTime); //cubeを動かす処理
        //=======================================
        //ダンス
        //=======================================
        if (Input.GetKeyDown("return") && controller.isGrounded)
        {
            SetDancing();
        }
    }
    //================================================
    //関数名:SetDancing
    //引き数:なし
    //戻り値:なし
    //================================================
    //通常状態ならダンス開始、ダンス中ならダンス終了
    //================================================
    public void SetDancing()
    {
        if (!animator.GetBool("isdancing"))
        {
            animator.SetBool("isdancing", true);
            Debug.Log("ダンス開始");
            return;
        }
        else
        {
            animator.SetBool("isdancing", false);
            Debug.Log("ダンス終了");
            return;
        }
    }

}