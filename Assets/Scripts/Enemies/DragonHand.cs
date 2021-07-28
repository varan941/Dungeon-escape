using UnityEngine;

public class DragonHand : MonoBehaviour, IDamageable
{
    [SerializeField] Transform target;
    [SerializeField] float followSpeed;        
    [SerializeField] Animator animator;

    public bool moveHand = true;

    public int Health { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {        
        if (moveHand)
        {
            float x = Mathf.Lerp(transform.parent.position.x, target.position.x, followSpeed * Time.deltaTime);
            transform.parent.position = new Vector3(x, transform.parent.position.y, transform.parent.position.z);
        }        
    }

    public void EnableMove()
    {        
        moveHand = true;
    }

    public void DisableMove()
    {        
        moveHand = false;
    }

    public void Damage()
    {
        Dragon.I.Damage();
    }

    public void SpeedUp(float animSpeed, float followSpeed)
    {        
        animator.SetFloat("SpeedDragonHit", animSpeed);
        this.followSpeed = followSpeed;
    }
}
