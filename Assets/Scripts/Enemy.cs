using Fusion;
using UnityEngine;

public class Enemy : NetworkBehaviour
{
    public GameObject Vfx;
    [Networked]
    private TickTimer life { get; set; }
    [SerializeField] float Speed;
    Animator anim;
    PlayerControler[] player;
    Vector3 tarhet;
    Vector3 Direction;
    int ii;
    private void Start()
    {
        player = FindObjectsOfType<PlayerControler>();
        anim = GetComponent<Animator>();
        ii=Random.Range(0,player.Length);   
    }
    private void Update()
    {
        player=FindObjectsOfType<PlayerControler>();
        tarhet = player[ii].transform.position;
            transform.position = Vector3.MoveTowards(transform.position, tarhet, Speed * Time.deltaTime);
            Direction =tarhet-transform.position;
            transform.forward = Direction;
        if (tarhet == null)
        {
            ii=Random.Range(0,player.Length);   
        }
    }
    public override void FixedUpdateNetwork()
    {
        if (life.Expired(Runner))
        {
            anim.SetFloat("ENE", 1);
           Runner.Spawn(Vfx,transform.position, Quaternion.identity, Object.StateAuthority, (Runner, oo) =>
           {
               oo.GetComponent<VfxSpaw>().Init();
            });
            Runner.Despawn(Object);
        }
    }
    public void creatlife()
    {
        life = TickTimer.CreateFromSeconds(Runner, 16f);
    }
}
 
    
