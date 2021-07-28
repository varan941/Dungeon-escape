using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    public int Health { get; set; }
    public GameObject _acidEffectPrefab;


    public override void Init()
    {
        base.Init();
        Health = base.health;
    }

    public void Damage()
    {
        if (_isDead == true) // из-за этих строк, труп нельзя бить 
        {
            return;
        }

        Health--; // уменьш. здор. при ударе
        if (Health < 1)
        {
            _isDead = true;
            _anim.SetTrigger("Death");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject; //выпад. димантика
            diamond.GetComponent<Diamond>().gems = base.gems; // присвоен. ему цены за врага
        }
    }



    public void Attack()
    {
        Instantiate(_acidEffectPrefab, transform.position, Quaternion.identity); // создаём соплю

    }

    public override void Update() // уникальная логика, поэтому не наследуем base.
    {

    }

    public override void Movement() // уникальное движение делаем, поэтому не наследуем base.
    {

    }

}
