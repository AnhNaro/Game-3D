using Fusion;
using UnityEngine;
using TMPro;

public class PlayerControler : NetworkBehaviour
{
    public TMP_Text txtNameplayer;
   public CharacterController _control;
    [SerializeField] float playermove = 6;
    public VariableJoystick Joytick;
    Vector3 Velo;
    public GameObject canvas;
   [Networked] int Hp { get; set; }
    bool checkdame;
    public override void Spawned()
    {
        Hp = 2;
    }
    private void FixedUpdate()
    {
        if (Hp <= 0)
        {
          //  Runner.Shutdown();
            Runner.Despawn(Object);
        }
        if (checkdame)
        {
            GetDameRpc();
            checkdame = false;
        }
    }
    private void Update()
    {
        float aa = Mathf.Clamp(transform.position.z, -8.7f, 13.4f);
        float bb = Mathf.Clamp(transform.position.x, -13.5f, 12.8f);
        float cc = Mathf.Clamp(transform.position.y, -1f, 6f);
        transform.position = new Vector3(bb,cc, aa);
    }
    public override void FixedUpdateNetwork()
    {
        if (HasStateAuthority == false) return;
        else
        {
            canvas.SetActive(true);
        }
        if (!_control.isGrounded)
        {
            Velo = new Vector3(0, -1f, 0);
        }
        Vector3 Direct =new Vector3(Joytick.Direction.x, 0.0f, Joytick.Direction.y) * playermove * Runner.DeltaTime;
        _control.Move(Direct + Velo * Runner.DeltaTime);
        if (Direct != Vector3.zero)
        {
            gameObject.transform.forward = Direct;
        }
    }
    [Rpc(RpcSources.All,RpcTargets.StateAuthority)]
    public void GetDameRpc()
    {
        Hp--;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            checkdame = true;
        }
    }
}
