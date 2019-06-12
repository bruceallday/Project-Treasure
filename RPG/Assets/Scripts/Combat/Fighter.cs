using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 5f;

        Health target;
        float timeSinceLastAttack = 0f;




        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if (target.IsDead()) return;
           
            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }



        private void AttackBehaviour()
        {
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                //This will trigger the Hit() event
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0f;

            }

        }

        // A N I M A T I O N  E V E N T 
        void Hit()
        {

          target.TakeDamage(weaponDamage);

        }





        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }






        public void Attack(Combat_Target combatTarget)
        {
            GetComponent<Action_Schedular>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        
        }




        public void Cancel()
        {
            GetComponent<Animator>().SetTrigger("stop_attack");
            target = null;
        }

    
    }
}


