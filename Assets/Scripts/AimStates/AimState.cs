using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimState : AimBaseState
{
    public override void EnterState(AimStateManger aim)
    {
        aim.anim.SetBool("Aiming", true);
        aim.currentFov = aim.adsFov;
    }
    
    public override void Update(AimStateManger aim)
    {
        if(Input.GetKeyUp(KeyCode.Mouse1)) aim.SwitchState(aim.Hip);
    }
}
