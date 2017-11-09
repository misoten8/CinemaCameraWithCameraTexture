using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class mobcharctor : MonoBehaviour
{
    //=========================================
    //構造体
    //=========================================
    public enum Mode
    {
        UROURO,//町をうろうろしている
        FOLLOW,//プレイヤーのファンになって追っかけている
        WAIT,  //静止状態
        EVENT, //イベントによって異な
        END
    };
    //=========================================
    //グローバル変数
    //=========================================
    public Mode m_Mode;
    private Animator animator;
    private bool Active;//updateの有効化on/off
    public Transform player;    //プレイヤーを代入
    public Transform goal0;
    public Transform goal1;
    public Transform goal2;
    public float speed = 1; //移動速度
    public float limitDistance = 0; //敵キャラクターがどの程度近づいてくるか設定(この値以下には近づかない）
    Vector3 oldPos;
    public int EventNum = 0;

    float changetime;   //動きを更新する時刻
    //=========================================
    //関数名:Start
    //戻り値:なし
    //引き数:なし
    //=========================================
    void Start()
    {

        m_Mode = Mode.UROURO;
        Active = true;
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        goal0  = GameObject.FindGameObjectWithTag("Goal0").transform;
        goal1  = GameObject.FindGameObjectWithTag("Goal1").transform;
        goal2  = GameObject.FindGameObjectWithTag("Goal2").transform;

        limitDistance = 10f;




    }
    //=========================================
    //関数名:Update
    //戻り値:なし
    //引き数:なし
    //=========================================
    void Update()
    {
        float temp = Vector3.Distance(oldPos, transform.position);

        switch (m_Mode)
        {

            case Mode.UROURO:
                if (changetime < Time.time)
                {
                    ////更新時刻が来た

                    int goal = UnityEngine.Random.Range(0, 2);    //ランダムで目的地を変える
                    int goal2 = UnityEngine.Random.Range(0, 2);    //ランダムで目的地を変える

                    GameObject goalobj = GameObject.Find("Goal" + goal);
                    GameObject goalobj2 = GameObject.Find("Goal" + goal2);
                    GameObject obj = GameObject.Find("Mob1");
                    GameObject obj2 = GameObject.Find("Mob2");
                    NavMeshAgent nma = obj.GetComponent<NavMeshAgent>();
                    NavMeshAgent nma2 = obj2.GetComponent<NavMeshAgent>();
                    nma.SetDestination(goalobj.transform.position);
                    nma2.SetDestination(goalobj2.transform.position);
                    changetime = Time.time + UnityEngine.Random.Range(15f, 20f);  //次の更新時刻を決める
        
                }
                //前とほぼ同じ位置だったら停止
                if (temp <= 0.001f)
                {
                    animator.SetBool("isrunning", false);
                    animator.SetBool("iswalking", false);

                }
                else
                { 
                    animator.SetBool("isrunning", false);
                    animator.SetBool("iswalking", true);
                }
                break;
            case Mode.FOLLOW:

                Vector3 playerPos = player.position;                 //プレイヤーの位置
                Vector3 direction = playerPos - transform.position; //方向と距離を求める。
                float distance = direction.sqrMagnitude;            //directionから距離要素だけを取り出す。
                direction = direction.normalized;                   //単位化（距離要素を取り除く）
                direction.y = 0f;                                   //後に敵の回転制御に使うためY軸情報を消去。これにより敵上下を向かなくなる。
                //プレイヤーの距離が一定以上でなければ、敵キャラクターはプレイヤーへ近寄ろうとしない
                if (distance >= limitDistance)
                {
                    animator.SetBool("iswalking", false);
                    animator.SetBool("isrunning", true);
                    //プレイヤーとの距離が制限値以上なので普通に近づく
                    transform.position = transform.position + (direction * speed * Time.deltaTime);

                }
                else if (distance < limitDistance)
                {
                    animator.SetBool("iswalking", false);
                    animator.SetBool("isrunning", false);
                }
                //プレイヤーの方を向く
                transform.rotation = Quaternion.LookRotation(direction);

                break;
            case Mode.EVENT:
                GameObject eventobj = GameObject.Find("Event" + EventNum);      
                GameObject Eobj = GameObject.Find("Mob1");
                GameObject Eobj2 = GameObject.Find("Mob2");
                NavMeshAgent Enma = Eobj.GetComponent<NavMeshAgent>();
                NavMeshAgent Enma2 = Eobj2.GetComponent<NavMeshAgent>();
                Enma.SetDestination(eventobj.transform.position);
                Enma2.SetDestination(eventobj.transform.position);
                Enma.speed = 1.5f;
                Enma2.speed = 1.5f;
                float eventrange = Vector3.Distance(oldPos, eventobj.transform.position);
                //前とほぼ同じ位置だったら停止
                if (eventrange <= 1f)
                {
                    animator.SetBool("isrunning", false);
                    animator.SetBool("iswalking", false);


                }
                else
                {
                    animator.SetBool("isrunning", true);
                    animator.SetBool("iswalking", false);
                }
                break;
   
        }

        //=======================================
        //モブの状態操作
        //=======================================
        if (Input.GetKeyDown("1"))
        {
            m_Mode = Mode.UROURO;
        }
        else if (Input.GetKeyDown("2"))
        {
            m_Mode = Mode.FOLLOW;
        }
        else if (Input.GetKeyDown("3"))
        {
            m_Mode = Mode.WAIT;
        }
        else if (Input.GetKeyDown("4"))
        {
            m_Mode = Mode.EVENT;
        }
        //=======================================
        //イベントの切り替え
        //=======================================
        if (Input.GetKeyDown("5"))
        {
            SetEvent();
        }


        //1フレーム前の位置を保存
        oldPos = transform.position;
    }
    //=========================================
    //関数名:SetMode
    //戻り値:なし
    //引き数:モブキャラに設定したい値
    //=========================================
    public void SetMode(Mode mode)
    {
        m_Mode = mode;

    }
    //=========================================
    //関数名:SetEvent
    //戻り値:なし
    //引き数:モブキャラに設定したい値
    //=========================================
    public void SetEvent()
    {
        if (EventNum == 0)
        {
            EventNum = 1;
            return;
        }
        else
        {
            EventNum = 0;
            return;
        }

    }
}