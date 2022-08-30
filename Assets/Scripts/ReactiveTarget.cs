using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    bool hit = false;
    public void ReactToHit()
    {
        WanderingAi enemyAi = GetComponent<WanderingAi>();

        if(enemyAi != null)
        {
            enemyAi.ChangeState(EnemyStates.dead);
            
            if(!hit)
            {
                Messenger.Broadcast(GameEvents.ENEMY_DEAD);
                hit = true;
            }
        }
        Animator anim = gameObject.GetComponent<Animator>();
        if(anim) {
            anim.SetTrigger("Die");
        }
    }

    private void DeadEvent() {
        Destroy(this.gameObject);
    }
}
