using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidEffect : MonoBehaviour
{
    public float _time=5.0f;
    private void Start()
    {
        Destroy(this.gameObject, _time); // уничтож. сопл. через 5 сек.
    }

    private void Update()
    {
        transform.Translate(Vector2.right*3*Time.deltaTime); // сопля двигает. вправо со скор. 3

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Player") // провер. задела ли сопля игрока
        {
            IDamageable hit = other.GetComponent<IDamageable>(); // берём комп. IDamageable

            if (hit!=null)
            {
                hit.Damage(); // наносим  урон
                Destroy(this.gameObject); // уичтожаем соплю
            }
        }
        
    }
  
}
