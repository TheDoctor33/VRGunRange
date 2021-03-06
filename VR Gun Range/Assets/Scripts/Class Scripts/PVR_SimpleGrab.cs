﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVR_SimpleGrab : PVR_InteractionObject
{
    public bool hideControllerModelOnGrab;
    
    private Rigidbody rb;
    private bool gripWasPressed;

    public override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
        gripWasPressed = false;
    }

    public override void OnGripWasPressed(PVR_InteractionController controller)
    {
        base.OnGripWasPressed(controller);

        if (hideControllerModelOnGrab)
        {
            controller.HideControllerModel();
        }
            AddFixedJointToController(controller);

    }

    public override void OnGripWasReleased(PVR_InteractionController controller)
    {
        base.OnGripWasReleased(controller);

        if (hideControllerModelOnGrab)
        {
            controller.ShowControllerModel();
        }

        RemoveFixedJointFromController(controller);
    }

    private void AddFixedJointToController(PVR_InteractionController controller)
    {
        FixedJoint fx = controller.gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        fx.connectedBody = rb;
    }

    private void RemoveFixedJointFromController(PVR_InteractionController controller)
    {
        if (controller.gameObject.GetComponent<FixedJoint>())
        {
            FixedJoint fx = controller.gameObject.GetComponent<FixedJoint>();
            fx.connectedBody = null;
            Destroy(fx);
        }

        //rb.velocity = controller.velocity; rb.angularVelocity = controller.angularVelocity;
    }
}
