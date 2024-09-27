
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]private float attackcooldown ;
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] fireballs;
    private Animator anim;
    private playermovement playermovement;
    private float cooldowntimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playermovement = GetComponent<playermovement>();
    }
    private void Update()
    {
        if (Input.GetMouseButton(0)&& cooldowntimer >attackcooldown && playermovement.canattack())
        {
            attack();
            
        }
        cooldowntimer += Time.deltaTime;
    }
    private void attack()

    {
        anim.SetTrigger("attack");
        cooldowntimer = 0;
        

        fireballs[findfireball()].transform.position = firepoint.position;
        fireballs[findfireball()].GetComponent<fire>().setdirection(Mathf.Sign(transform.localScale.x));
    }
    private int findfireball()
    {
        for (int i = 0; i < fireballs.Length; i++) {
            if (!fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}
