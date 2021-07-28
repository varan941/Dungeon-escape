using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieArea : MonoBehaviour, IDamageable
{
    public int Health { get; set; }

    public void Damage()
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            IDamageable hit = other.GetComponent<IDamageable>(); // берём комп. IDamageable

            if (hit != null)
            {
                hit.Damage(); // наносим  урон
                hit.Damage();
                hit.Damage();
                hit.Damage();
            }
        }
    }
}
