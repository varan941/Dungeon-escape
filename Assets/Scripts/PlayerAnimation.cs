using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;
    private Animator _swordAnimation;
    public bool IsDead;
    
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _swordAnimation = transform.GetChild(1).GetComponent < Animator>(); // ыозьмёт аниматор у другого ребёнка
        
    }

    public void Move(float move)
    {
        _anim.SetFloat("Move", Mathf.Abs(move)); // Mathf.Abs(move) чтобы аним. не уходлиа в idle когда move =-1

    }

    public void Jump(bool jumping)
    {
        _anim.SetBool("Jumping", jumping);
        
    }

    public void Attack()
    {
        _anim.SetTrigger("Attack");
        _swordAnimation.SetTrigger("Sword_Anim");   
    }

    public void Death()
    {
        _anim.SetTrigger("Death");
        _anim.SetBool("IsDead", true);
        IsDead=true;
    }
}
