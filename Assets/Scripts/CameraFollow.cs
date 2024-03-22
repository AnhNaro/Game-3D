using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    PlayerControler player;
    public Vector2 LimitX;
    public Vector2 LimitZ;
    Vector3 Direct;
    Vector3 ZZe;
    [Range(0f, 1f)]
    [SerializeField] float Radius;
    void Update()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerControler>();
            transform.position = new Vector3(player.transform.position.x, transform.position.y,player.transform.position.z-5 );
            Direct = transform.position - player.transform.position;
        }
        Vector3 mostarget=player.transform.position+Direct;
        mostarget.x=Mathf.Clamp(mostarget.x,LimitX.x,LimitX.y);
        mostarget.z=Mathf.Clamp(mostarget.z,LimitZ.x,LimitZ.y);
        transform.position=Vector3.SmoothDamp(transform.position,mostarget,ref ZZe,Radius);
    }
}
