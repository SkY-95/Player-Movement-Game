using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camara : MonoBehaviour
{
   [SerializeField] private float speed;
    [SerializeField] private float currentposx;
    [SerializeField] private Transform player;
    private Vector3 velocity=Vector3.zero;
    [SerializeField] private float camdistance;
    [SerializeField] private float camspeed;
    private float lookahead;

    private void Update()
    {
        // transform.position=Vector3.SmoothDamp(transform.position,new Vector3(currentposx,transform.position.y,transform.position.z), ref velocity,speed);
        transform.position = new Vector3(player.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        lookahead=Mathf.Lerp(lookahead, (camdistance*player.localScale.x),Time.deltaTime*camspeed);
    }
    public void MoveToNewRoom(Transform _newroom)
    {
        currentposx = _newroom.position.x;
    }
}
