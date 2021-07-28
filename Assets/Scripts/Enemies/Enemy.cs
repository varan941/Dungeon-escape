using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected int health;
    [SerializeField] protected float speed;
    [SerializeField] protected int gems;
    [SerializeField] protected Transform pointA, pointB;
    [SerializeField] protected float hitDistance = 3f;
    private float _idleTime = 1.4f;


    protected SpriteRenderer _enemysprite;
    protected Vector2 _currentTarget;
    protected Vector2 _raycastSide = new Vector2(1, 0);    
    protected Animator _anim;
    protected Player player;

    private bool _isHit = false;
    protected bool _isDead = false;
    protected bool _inCombat;
    public bool _canMove = true;

    public GameObject diamondPrefab;

    public int Health { get => health; set => health = value; }
    protected bool InCombat
    {
        get => _inCombat;
        set
        {
            if (_inCombat && value == false)
            {
                StartCoroutine(ResetMove());
                TurnSprite(CurrentTarget.x == pointA.position.x);
            }
            _inCombat = value;
            _anim.SetBool("InCombat", value);
        }
    }

    protected Vector2 CurrentTarget
    {
        get => _currentTarget;
        set
        {
            _currentTarget = value;
            TurnSprite(value.x == pointA.position.x);
            _canMove = false;
            _anim.SetTrigger("Idle");

            StartCoroutine(ResetMove());
        }
    }   

    protected bool IsHit
    {
        get => _isHit;
        set
        {
            _isHit = value;

            if (value)
            {
                _canMove = false;
                TurnToPlayer();
            }
        }
    }

    public virtual void Init()
    {
        _enemysprite = GetComponentInChildren<SpriteRenderer>();
        _anim = GetComponentInChildren<Animator>();
        player = Player.I;

        var clipArray = _anim.runtimeAnimatorController.animationClips;
        foreach (AnimationClip item in clipArray)
        {
            if (item.name == "Idle")
            {
                _idleTime = item.length;
            }
        }


    }

    private void Start()
    {
        Init();

    }

    public virtual void Update()
    {
        //if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && _anim.GetBool("InCombat") == false) //проверяет работает ли анимация Idle на слое 0 и выкл. боев. реж.        
        //    return; //проверяет еще раз; пока не станет ложь, не преходит к следущему коду        

        DetectPlayer();

        Movement();
    }

    public virtual void DetectPlayer()
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, transform.TransformDirection(_raycastSide), hitDistance);
        Debug.DrawRay(transform.position, transform.TransformDirection(_raycastSide * hitDistance), Color.white);
        if (raycastHit2D.collider!=null&&raycastHit2D.collider.tag=="Player")
        {
            Debug.Log("detected player");
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

    public virtual void Movement()
    {
        if (_isDead == true || _canMove == false) //проверяет не умер ли враг
            return;

        if (transform.position == pointA.position)
        {
            CurrentTarget = pointB.position;
            _raycastSide.x = 1;
        }

        if (transform.position == pointB.position)
        {
            CurrentTarget = pointA.position;
            _raycastSide.x = -1;
        }
         
        transform.position = Vector2.MoveTowards(transform.position, CurrentTarget, speed * Time.deltaTime);
    }

    public virtual void Damage()
    {
        if (_isDead == true)
        {
            return;
        }

        Health--;
        //Debug.Log(Health);
        _anim.SetTrigger("Hit");
        IsHit = true;
        _anim.SetBool("InCombat", true);

        if (Health < 1)
        {
            _isDead = true;
            _anim.SetTrigger("Death");
            gameObject.GetComponent<BoxCollider2D>().enabled = false; // удаляет коллайдер после смерти

            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject; //выпад. димантика
            diamond.GetComponent<Diamond>().gems = gems; // присвоен. ему цены за врага

            GameManager.I.InvokeDecorations();
        }
    }

    private void TurnSprite(bool needTurn)
    {
        _enemysprite.flipX = needTurn;
        if (needTurn)
            _raycastSide.x = -1;
        else
            _raycastSide.x = 1;
    }

    private void TurnToPlayer()
    {
        Vector2 direction = player.transform.localPosition - transform.localPosition; //направ. меж иг. и враг.
        TurnSprite(direction.x < 0);
        
        _anim.SetTrigger("Hit");
    }

    public IEnumerator ResetMove()
    {
        //Debug.Log("идл анимация: " + _idleTime);
        yield return new WaitForSeconds(_idleTime);
        _canMove = true;
    }

}
