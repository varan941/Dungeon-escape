using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput; // чтоб реализовать джойстик

public class Player : Singleton<Player>, IDamageable
{
    private Rigidbody2D _rigid;

    [SerializeField] int health;
    public float speed;
    [SerializeField] private float _jumpforce = 5.0f;
    [SerializeField] private bool _resetJump = false;    
    [SerializeField] float _cooldownAttack = 1f;
    [SerializeField] int _attackAttempts = 2;

    private PlayerAnimation _playeranim;
    private SpriteRenderer _playersprite;
    private SpriteRenderer _swordArcsprite;

    private bool _grounded = false;
    private bool _resetAttackStart = false;

    public int diamonds;       

    public int Health { get => health; set => health = value; } // реализуем IDamageable интерфейс    

    public int AttackAttempts
    {
        get => _attackAttempts;
        set
        {
            _attackAttempts = value;
            UI_Manger.I.UpdateAttemptsAttack(_attackAttempts.ToString());
        }
    }

    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _playeranim = GetComponent<PlayerAnimation>();
        _playersprite = GetComponentInChildren<SpriteRenderer>();
        _swordArcsprite = transform.GetChild(1).GetComponent<SpriteRenderer>();       

    }

    void Update()
    {
        Movement();
        IsGrounded();

        if (CrossPlatformInputManager.GetButtonDown("A_Button") && IsGrounded() == true && _playeranim.IsDead == false) // атака
        {
            if (AttackAttempts > 0)
            {
                _playeranim.Attack();
                Attack();
            }
        }
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Q) && IsGrounded() == true && _playeranim.IsDead == false) // атака
        {
            if (AttackAttempts > 0)
            {
                _playeranim.Attack();
                Attack();
            }
        }
#endif

    }

    void Movement()
    {
#if UNITY_EDITOR
        float move = Input.GetAxisRaw("Horizontal"); // raw чтоб убрать smooth
#else
        float move = CrossPlatformInputManager.GetAxisRaw("Horizontal"); 
#endif

        Flip(move);
        _grounded = IsGrounded(); // constantly casting raycast


        if ((Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("B_Button")) && IsGrounded() == true && _playeranim.IsDead == false)
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpforce);
            StartCoroutine(ResetJumpRoutine());
            _playeranim.Jump(true);
        }


        if (_playeranim.IsDead == false)  // если IsDead==false, перс может ходить
        {
            _rigid.velocity = new Vector2(move * speed, _rigid.velocity.y);
            _playeranim.Move(move); // запускает анимацию
        }


    }

    bool IsGrounded()
    {
        RaycastHit2D hitinfo = Physics2D.Raycast(transform.position, Vector2.down, 0.9f, 1 << 8);
        // для того чтобы пускать луч к земле; 1<<8 проверяет 8 слой "Землю"; длина луча 0.6f
        Debug.DrawRay(transform.position, Vector2.down * 0.9f, Color.green);

        if (hitinfo.collider != null)
        {
            if (_resetJump == false)
            {
                _playeranim.Jump(false);
                return true;
            }
        }
        return false;
    }

    void Flip(float move)
    {
        if (_playeranim.IsDead == false)
        {
            if (move > 0)
            {
                //_playersprite.flipX = false;  // не подходит потому что Hit_box меча завязан на анимации, значит надо менять поз-ю
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                _swordArcsprite.flipX = false;
                _swordArcsprite.flipY = false;

                Vector3 newPos = _swordArcsprite.transform.localPosition; //как обычно хрен его знает
                newPos.x = 1.01f;                                         //по моему чтобы меч не криво менял позицию
                _swordArcsprite.transform.localPosition = newPos;
            }
            else if (move < 0)
            {
                //_playersprite.flipX = true;
                gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                //_swordArcsprite.flipX = true; //некрасиво просто если включено
                _swordArcsprite.flipY = true;

                Vector3 newPos = _swordArcsprite.transform.localPosition;
                newPos.x = -1.01f;
                _swordArcsprite.transform.localPosition = newPos;
            }
        }
    }


    public void Damage() // реализуем IDamageable интерфейс
    {
        if (Health < 1) 
        {
            return;
        }        
        Health--;
        UI_Manger.I.UpdateLives(Health); // кидаем в ЮА наше хп

        if (Health < 1)
        {
            _playeranim.Death();  // запуск метода, которЫй запуск аним смерти
        }
    }

    public void Attack()
    {
        if (AttackAttempts > 0)
        {
            //Debug.Log("player attack");
            int maxAttempts = AttackAttempts;
            AttackAttempts--;            

            if (!_resetAttackStart)
                StartCoroutine(ResetAttackCrt(maxAttempts));
        }
        else
            Debug.Log("wait");
    }

    public void AddGems(int amount)
    {
        diamonds += amount; // добавл. к общему колич. диамант. 1
        UI_Manger.I.UpdateGemcount(diamonds);
    }

    IEnumerator ResetJumpRoutine() //couroutine for reset jump
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }

    IEnumerator ResetAttackCrt(int maxAttempts)
    {
        _resetAttackStart = true;
        //Debug.Log("ResetAttackCrt");
        while (AttackAttempts < maxAttempts)
        {
            yield return new WaitForSeconds(_cooldownAttack);
            AttackAttempts++;
        }
        _resetAttackStart = false;


    }



}
//void CheakGrounded()
//{
//    RaycastHit2D hitinfo = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, 1 << 8); //для того чтобы пускать луч 
//                                                                                              //к земле; 1<<8 проверяет 8 слой "Землю"; длина луча 0.6f
//    Debug.DrawRay(transform.position, Vector2.down * 0.6f, Color.green);
//    if (hitinfo.collider != null)
//    {
//        Debug.Log("Hit " + hitinfo.collider.name);
//        if (resetJumpNeeded == false)
//        {
//            _grounded = true;
//        }
//    }
//}


//IEnumerator ResetJumpNeeded() //couroutine for reset jump
//{
//    yield return new WaitForSeconds(1.0f);
//    resetJumpNeeded = false;
//}

//_rigid.velocity = new Vector2(_rigid.velocity.x, _jumpforce);
//_grounded = false;
//            resetJumpNeeded = true;
//            StartCoroutine(ResetJumpNeeded());
