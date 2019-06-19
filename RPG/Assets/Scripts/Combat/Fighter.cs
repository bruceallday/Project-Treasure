using UnityEngine;
using RPG.Movement;
using RPG.Core;
using System;

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

            if (target.IsDead())
            {
                Cancel();
                return;
            }

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position, 1f);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }


        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                //This will trigger the Hit() event
                TriggetAttack();
                timeSinceLastAttack = Mathf.Infinity;

            }

        }

        private void TriggetAttack()
        {
            GetComponent<Animator>().ResetTrigger("stop_attack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        // A N I M A T I O N  E V E N T 
        void Hit()
        {
            if (target == null) { return; }
            target.TakeDamage(weaponDamage);

        }


        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }



        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) { return false; }
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }


        public void Attack(GameObject combatTarget)
        {
            GetComponent<Action_Schedular>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        
        }




        public void Cancel()
        {
            ResetAttack();
            target = null;
            GetComponent<Mover>().Cancel();
        }

        private void ResetAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stop_attack");
        }

    }
}


