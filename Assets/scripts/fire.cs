
using Unity.Mathematics;
using UnityEngine;

public class fire : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool hit;
    private BoxCollider2D boxcollider;
    private Animator anim;
    private float direction;
    private float lifetime;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxcollider = GetComponent<BoxCollider2D>();

    }
    private void Update()
    {
        if (hit)
        {
            return;
        }
        float movementspeed = speed * Time.deltaTime * direction;
        transform.Translate(movementspeed, 0, 0);
        lifetime += Time.deltaTime;
        if (lifetime > 5)
        {
            gameObject.SetActive(false);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxcollider.enabled = false;
        anim.SetTrigger("expload");
    }
    public void setdirection(float _direction)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxcollider.enabled = true;
        float localscaleX = transform.localScale.x;
        if (Mathf.Sign(localscaleX) != direction)
        {
            localscaleX = -localscaleX;
        }
        transform.localScale = new Vector3(localscaleX, transform.localScale.y, transform.localScale.z);
    }
    private void deactivate()
    {
        gameObject.SetActive(false);
    }
}