using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

	[SerializeField]
	protected int health;
	[SerializeField]
	protected int speed;
	[SerializeField]
	protected int gems;
	[SerializeField]
	protected Transform pointA, pointB;
	public GameObject diamondPrefab;
	protected Vector3 currentTarget;
	protected Animator animator;
	protected SpriteRenderer sprite;
	protected bool isHit = false;
	protected Player player;
	protected bool isDead = false;


	public virtual void Init(){
		animator = GetComponentInChildren<Animator>();
		sprite = GetComponentInChildren<SpriteRenderer>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	void Start(){
		Init();
	}

	public virtual void Update(){
		if(this.animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && animator.GetBool("InCombat") == false){
			return;
		}
		if(isDead == false){
			Movement();
		}
	}

	public virtual void Movement(){
		if(this.currentTarget == pointA.position){
			this.sprite.flipX = true;
		}
		else if(this.currentTarget == pointB.position){
			this.sprite.flipX = false;
		}
		if(this.transform.position == pointA.position){
			animator.SetTrigger("Idle");
			currentTarget = pointB.position;
		}	
		else if(this.transform.position == pointB.position){
			animator.SetTrigger("Idle");	
			currentTarget = pointA.position;
		}
		if(isHit == false){
			transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
		}

		float distance = Vector3.Distance(transform.localPosition, player.transform.localPosition);
		if(distance > 2.0f){
			isHit = false;
			animator.SetBool("InCombat", false);
		}
		Vector3 direction = player.transform.localPosition - transform.localPosition;

		if(direction.x > 0 && animator.GetBool("InCombat") == true){
			sprite.flipX = false;
		}
		else if(direction.x < 0 && animator.GetBool("InCombat") == true){
			sprite.flipX = true;
		}
	
	}


	
}
