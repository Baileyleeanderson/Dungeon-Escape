using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable {

    public int Health { get; set; }
    public GameObject acidEffectPrefab;

    public override void Init(){
        base.Init();
        Health = base.health;
    }

    public void Damage(){
        if(isDead == true){
            return;
        }
        Health -= 1;
        this.health -= 1;
        if(Health < 1){
            isDead = true;
            animator.SetTrigger("Death");
            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject;
            diamond.GetComponent<Diamond>().gems = base.gems;
        }
    }

    public override void Update(){
        
    }

    public override void Movement(){

    }

    public void Attack(){
        Instantiate(acidEffectPrefab, transform.position, Quaternion.identity);
    }
}
