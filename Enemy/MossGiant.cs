﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy, IDamageable {

    public int Health { get; set; }

    public override void Init(){
        base.Init();
        Health = base.health;
    }

    public void Damage(){
        if(isDead == true){
            return;
        }

        Debug.Log("Damage");
        Health -= 1;
        animator.SetTrigger("Hit");
        isHit = true;
        animator.SetBool("InCombat", true);
        if(Health < 1){
            isDead = true;
            animator.SetTrigger("Death");
            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject;
            diamond.GetComponent<Diamond>().gems = base.gems;
        }
    }
    	
}
