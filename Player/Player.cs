using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public int diamonds;
    private Rigidbody2D _rigid;
    [SerializeField] private float _jumpForce = 5.0f;
    private bool _resetJump = false;
    [SerializeField] private float speed = 3.0f;
    private Anim _playerAnim;
    private SpriteRenderer _playerSprite;
    private SpriteRenderer _swordArcSprite;
    private bool _grounded = false;
    public int h;
    private bool isDead = false;
    

    public int Health { get; set; }
    void Start()
    {
        Health = h;
        _rigid = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<Anim>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if (Input.GetMouseButtonDown(0) && IsGrounded() == true && isDead == false)
            _playerAnim.Attack();
    }

    
    void Movement()
    {
        if (isDead == true)
            return;
        else
        {
            float move = Input.GetAxisRaw("Horizontal");
            _grounded = IsGrounded();
            if (move > 0)
                Flip(true);
            else if (move < 0)
                Flip(false);
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() == true)
            {
                _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
                StartCoroutine(ResetJumpRoutine());
                _playerAnim.Jump(true);
            }
            _rigid.velocity = new Vector2(move * speed, _rigid.velocity.y);
            _playerAnim.Move(move);
        }
    }
    bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1f, 1 << 8);
        Debug.DrawRay(transform.position, Vector2.down, Color.green);
        if (hitInfo.collider != null)
        {
            if (_resetJump == false)
            {
                _playerAnim.Jump(false);
                return true;
            }
        }
        return false;
    }
    void Flip(bool faceRight)
    {
        if (faceRight == true)
        {
            _playerSprite.flipX = false;
            _swordArcSprite.flipX = false;
            _swordArcSprite.flipY = false;
            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = 1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
        else if (faceRight == false)
        {
            _playerSprite.flipX = true;
            _swordArcSprite.flipX = true;
            _swordArcSprite.flipY = true;
            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = -1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
    }
    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }
    public void Damage()
    {
        if (Health < 1)
        {
            isDead = true;
            return;
        }
        Debug.Log("Player : Damage()");
        Health--;
        UIManager.Instance.UpdateLives(Health);
        if (Health < 1)
        {
            
            _playerAnim.Death();
        }
    }
    public void AddGems(int amount)
    {
        diamonds += amount;
        UIManager.Instance.UpdateGem(diamonds);
    }
}

