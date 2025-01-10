using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HipFireState : AimBaseState
{
    public override void EnterState(AimStateManger aim)
    {
        aim.anim.SetBool("Aiming", false);
        aim.currentFov = aim.hipFov;
    }

    public override void Update(AimStateManger aim)
    {
        if(Input.GetKey(KeyCode.Mouse1)) aim.SwitchState(aim.Aim);
    }
}
