using Fusion;
using UnityEngine;

public class VfxSpaw : NetworkBehaviour
{
    [Networked] TickTimer life { get; set; }
    public void Init()
    {
        life = TickTimer.CreateFromSeconds(Runner, 1.2f);
    }
    public override void FixedUpdateNetwork()
    {
        if (life.Expired(Runner))
        {
            Runner.Despawn(Object);
        }
    }
}
