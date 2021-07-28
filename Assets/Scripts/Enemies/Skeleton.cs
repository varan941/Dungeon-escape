using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    public int Health { get; set; }

    public override void Init()
    {
        base.Init();
        Health = base.health;

    }

    //public override void Damage()
    //{

    //    if (isDead == true)
    //    {
    //        return;
    //    }

    //    Health--;
    //    Debug.Log(Health);
    //    _anim.SetTrigger("Hit");
    //    isHit = true;
    //    _anim.SetBool("InCombat", true);

    //    if (Health<1)
    //    {

    //        isDead = true;
    //        _anim.SetTrigger("Death");
    //        gameObject.GetComponent<BoxCollider2D>().enabled = false; // удаляет коллайдер после смерти

    //        GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject; //выпад. димантика
    //        diamond.GetComponent<Diamond>().gems = base.gems; // присвоен. ему цены за врага

    //    }
        
    //}

    public override void Movement()
    {
        base.Movement();
    }

}
