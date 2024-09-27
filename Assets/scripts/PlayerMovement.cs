using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower=10;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxcollider;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private float walljumpcooldown;
    private float HorizontalInput;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxcollider = GetComponent<BoxCollider2D>();
    }
    private void Update()

    {
        if (Input.GetKey(KeyCode.Escape)) { 
            Application.Quit();
        }
        HorizontalInput = Input.GetAxis("Horizontal");
   
        if (HorizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;    
        }
        else if (HorizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1,1,1);
        }



        if (walljumpcooldown > 0.2f)
        {

            
            body.velocity = new Vector2(HorizontalInput * speed, body.velocity.y);
            if (onwall() && !isgrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
            {
                body.gravityScale = 3;

            }
            if (Input.GetKey(KeyCode.Space))
            {
                jump();
            }
        }
        else walljumpcooldown += Time.deltaTime;

        anim.SetBool("run", HorizontalInput != 0);
        anim.SetBool("grounded",isgrounded());
    }
    private void jump()
    {
        if (isgrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");

        }
        else if (onwall() && !isgrounded())
        {
            if (HorizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale= new Vector3(-Mathf.Sign(transform.localScale.x),transform.localScale.y,transform.localScale.z);
            }
            else body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 10);
            walljumpcooldown = 0;
         
        }

    }
 
    private bool isgrounded()
    {
        RaycastHit2D raycasthit = Physics2D.BoxCast(boxcollider.bounds.center,boxcollider.bounds.size,0,Vector2.down,0.1f,groundLayer);
        return raycasthit.collider!=null;
    }
    private bool onwall()
    {
        RaycastHit2D raycasthit = Physics2D.BoxCast(boxcollider.bounds.center, boxcollider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, wallLayer);
        return raycasthit.collider != null;
    }

    public bool canattack()
    {
        return HorizontalInput==0 && isgrounded() && !onwall();
    }
}
