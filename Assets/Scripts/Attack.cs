using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool _canDamage = true;
    private IEnumerator _crtResetDamage;
    [SerializeField] float attackTime = 0.1f;



    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Attack:"+other.name);

        IDamageable hit = other.GetComponent<IDamageable>();
        if (hit != null)
        {
            if (gameObject.layer == 9 ) //9 - sword (слой игрока)             
                PlayerAttack(hit);            
            else            
                EnemyAttack(hit);
        }
    }

    private void EnemyAttack(IDamageable hit)
    {
        if (_canDamage == true)
        {
            hit.Damage();
            _canDamage = false;
            Invoke(nameof(ResetAttack), attackTime);
        }
    }

    private void PlayerAttack(IDamageable hit)
    {
        if (_canDamage)
        {
            //Player.I.Attack(hit);
            hit.Damage();
            _canDamage = false;
            Invoke(nameof(ResetAttack), attackTime);
        }             
    }

    private void ResetAttack()
    {
        //Debug.Log(gameObject.transform.parent.name + " can attack");
        _canDamage = true;
    }


}
