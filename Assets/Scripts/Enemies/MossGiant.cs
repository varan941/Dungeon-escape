using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy
{
    [Header("Уникальные параметры")]
    [SerializeField] Transform rayStartPoint;

    public int Health { get; set; } 

    public override void Init()
    {
        base.Init();
        Health = base.health;

    }

    public void Damage()
    {
        if (_isDead == true) // если враг умер, программа нничего не делает
        {
            return;
        }
        Health--;
        Debug.Log("DAMAGE");
        _anim.SetTrigger("Hit");
        IsHit = true;
        _anim.SetBool("InCombat", true);

        if (Health < 1)
        {
            _isDead = true;
            _anim.SetTrigger("Death");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject; //выпад. димантика
            diamond.GetComponent<Diamond>().gems = base.gems; // присвоен. ему цены за врага
            
        }

    }

    public override void Movement()
    {
        base.Movement();
    }

    public override void DetectPlayer()
    {
        Debug.Log("TYT");
        RaycastHit2D raycastHit2D = Physics2D.Raycast(rayStartPoint.position, transform.TransformDirection(_raycastSide), hitDistance);
        Debug.DrawRay(rayStartPoint.position, transform.TransformDirection(_raycastSide * hitDistance), Color.white);
        if (raycastHit2D)
        {
            //_isHit = false;
            _canMove = false;
            InCombat = true;
        }
        else
        {
            InCombat = false;
            IsHit = false;
        }
    }
}

// не самый лучший способ
//void Movement()
//{
//    if (transform.position == pointA.position)
//    {
//        _switchMove = false;
//    }
//    else if (transform.position == pointB.position)
//    {
//        _switchMove = true;
//    }

//    if (_switchMove == false)
//    {
//        transform.position = Vector2.MoveTowards(transform.position, pointB.position, Time.deltaTime * speed);
//    }

//    else if (_switchMove == true)
//    {
//        transform.position = Vector2.MoveTowards(transform.position, pointA.position, Time.deltaTime * speed);
//    }

//}
