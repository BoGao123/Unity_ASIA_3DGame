using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("移動速度"), Range(0, 50)]
    public float speed = 3;
    [Header("停止距離"), Range(0, 50)]
    public float stopDistance = 3f;
    [Header("攻擊冷卻"), Range(0, 50)]
    public float cd = 3f;
    [Header("攻擊中心點")]
    public Transform atkPoint;
    [Header("攻擊長度"),Range(0f, 5f)]
    public float atkLength;
    [Header("攻擊力"), Range(0, 500)]
    public float atk = 30;


    private Transform player;
    private NavMeshAgent nav;
    private Animator ani;


    private float timer;
    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();

        player = GameObject.Find("機器人").transform;

        nav.speed = speed;
        nav.stoppingDistance = stopDistance;
    }

    private void Update()
    {
        Track();
        Attack();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawRay(atkPoint.position, atkPoint.forward * atkLength);
    }

    private RaycastHit hit;

    public float hp = 100; 
    private void Attack()
    {
        if(nav.remainingDistance < stopDistance)
        {
            timer += Time.deltaTime;

            Vector3 pos = player.position;

            pos.y = transform.position.y;

            transform.LookAt(pos);


            if (timer >= cd)
            {
                ani.SetTrigger("攻擊開關");
                timer = 0;


                if(Physics.Raycast(atkPoint.position, atkPoint.forward , out hit,atkLength, 1 << 8))
                {
                    hit.collider.GetComponent<Player>().Damage(atk);
                }
            }

            
        }

    }

    public void Damage(float damage)
    {
        hp -= damage;
        ani.SetTrigger("受傷開關");

        if (hp <= 0)
        {
            Dead();
        }

    }

    private void Dead()
    {
        nav.isStopped = true;
        enabled = false;
        ani.SetBool("死亡觸發", true);      
    }

    private void Track()
    {
        nav.SetDestination(player.position);

        ani.SetBool("跑步開關", nav.remainingDistance > stopDistance);
    }



}
