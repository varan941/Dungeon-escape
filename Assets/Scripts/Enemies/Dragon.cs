using UnityEngine;

public class Dragon : Singleton<Dragon>
{
    [SerializeField] DragonPhase phase;
    [SerializeField] Transform target;    
    [SerializeField] DragonHand[] hands = new DragonHand[4];

    public int _health;
    private bool _isDead;
    public Animator anim;

    public int Health
    {
        get { return _health; }
        set 
        {
            _health = _health+ value;
            CheckHealth();
        }
    }


    public void Damage()
    {
        Debug.Log("Урон по дракону");

        if (_isDead)        
            return;

        Health = - 1;        
    }

    public void CheckHealth()
    {
        if (Health<10&& phase==DragonPhase.First)
        {            
            phase = DragonPhase.Second;
            hands[1].transform.parent.gameObject.SetActive(true);
                
            foreach (var item in hands)
            {
                item.SpeedUp(1.2f, 1.2f);
            }
        }
    }

}

public enum DragonPhase
{
    First,
    Second,
    Third
}
