
using UnityEngine;

public class door : MonoBehaviour
{
    [SerializeField] private Transform nextroom;
    [SerializeField] private Transform previousroom;
    [SerializeField] private camara cam;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.transform.position.x < transform.position.x)
                cam.MoveToNewRoom(nextroom);
            else
                cam.MoveToNewRoom(previousroom);
        }
    }
}
