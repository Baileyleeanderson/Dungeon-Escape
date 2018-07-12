using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable {

	private Rigidbody2D _rigid;
	[SerializeField]
	private float _jumpForce = 3.0f;
	[SerializeField]
	private bool _grounded = false;
	[SerializeField] 
	private float _speed = 5.0f;
	private bool _resetJumpNeeded = false;
	[SerializeField]
	private PlayerAnimation _playerAnim;
	private SpriteRenderer _playerSprite;
	private Animator _playerAnimator;
	private SpriteRenderer _swordArcSprite;
	[SerializeField]
	private int health;
	public int Health { get; set; }
	public int diamonds;

	void Start () {
		_rigid = GetComponent<Rigidbody2D>();
		_playerAnim = GetComponent<PlayerAnimation>();
		_playerSprite = GetComponentInChildren<SpriteRenderer>();
		_swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
		Health = 4;
	}
	
	void Update () {
		Movement();
		CheckGrounded();	
	}
	
	void Movement(){
		float move = Input.GetAxisRaw("Horizontal");

		Flip(move);

		if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Mouse0) && _grounded){
			_playerAnim.Attack();
		}

		if(Input.GetKeyDown(KeyCode.Space) && _grounded == true){
			_rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
			_grounded = false;
			_resetJumpNeeded = true;
			StartCoroutine(ResestJumpNeededRoutine());
			_playerAnim.Jump(true);
			
		}
		_rigid.velocity = new Vector2(move * _speed, _rigid.velocity.y);
		_playerAnim.Move(move);
	}

	void CheckGrounded(){
		RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1.2f, 1 << 8);
		Debug.DrawRay(transform.position, Vector2.down * 1.2f, Color.green);

		if(hitInfo.collider != null){
			Debug.Log("Hit" + hitInfo.collider.name);
			if(_resetJumpNeeded == false){
				_grounded = true;
				_playerAnim.Jump(false);
			}
		}
	}

	void Flip(float move){
		if(move > 0){
			_playerSprite.flipX = false;
			_swordArcSprite.flipX = false;
			_swordArcSprite.flipY = false;
			Vector3 newPos = _swordArcSprite.transform.localPosition;
			newPos.x = 1.04f;
			_swordArcSprite.transform.localPosition = newPos;
		}
		else if(move < 0){
			_playerSprite.flipX = true;
			_swordArcSprite.flipX = true;
			_swordArcSprite.flipY = true;
			Vector3 newPos = _swordArcSprite.transform.localPosition;
			newPos.x = -1.04f;
			_swordArcSprite.transform.localPosition = newPos;
		}
	}

	IEnumerator ResestJumpNeededRoutine(){
		yield return new WaitForSeconds(0.1f);
		_resetJumpNeeded = false;
	}	

	public void Damage(){

		if(Health < 1){
			return;
		}
		Health --;
		Debug.Log("PlayerDamaged");
		UIManager.Instance.UpdateLives(Health);
		if(Health < 1){
			_playerAnim.Death();

		}
	}

	public void AddGems(int amount){
		diamonds += amount;
		UIManager.Instance.UpdateGemCount(diamonds);
	}

}
