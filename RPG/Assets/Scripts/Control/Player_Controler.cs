using System;

using RPG.Movement;
using UnityEngine;

using RPG.Combat;

namespace RPG.Control
{
    public class Player_Controler : MonoBehaviour
    {
        private void Update()
        {
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
               Combat_Target target = hit.transform.GetComponent<Combat_Target>();
                if (target == null) continue;

                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target);
                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point);
                }
                return true;
            }
            return false;

            //Using the Debug we can view the ray. From the ray origin in the direction
            //Debug.DrawRay(lastray.origin, lastray.direction * 100);
        }

        private static Ray GetMouseRay()
        {
            
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}



