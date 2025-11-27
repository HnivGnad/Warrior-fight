using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityState
{
    protected Animator anim;
    protected Rigidbody2D rb;
    protected Player player;
    protected StateMachine stateMachine;
    protected string animBoolName;
    protected PlayerInputSet input;

    public EntityState (Player player, StateMachine stateMachine, string stateName) {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = stateName;

        anim = player.anim;
        rb = player.rb;
        input = player.input;
    }

    public virtual void Enter() {
        anim.SetBool(animBoolName, true);
    }
    public virtual void Update() {
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
    }
    public virtual void Exit() {
        anim.SetBool(animBoolName, false);
    }
}
