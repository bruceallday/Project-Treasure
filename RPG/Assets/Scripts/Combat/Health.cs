using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100f;

        bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {

            healthPoints = Mathf.Max(healthPoints - damage, 0);
            print(healthPoints);

            if (healthPoints == 0)
            {
                print("should be dead");
                Die();
            }

        }

        private void Die()
        {
            if (isDead == true) return;
            {
                isDead = true;
                GetComponent<Animator>().SetTrigger("die");

            }

        }
    }
}

