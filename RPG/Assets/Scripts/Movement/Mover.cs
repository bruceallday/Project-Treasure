using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] Transform target;

        NavMeshAgent navMeshAgent;

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateAnimator();
        }


        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<Action_Schedular>().StartAction(this);
            MoveTo(destination);
        }

        // M O V E M E N T  C O N T R O L L E R 

        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }


        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        // A N I M A T O R  C O N T R O L L E R 

        private void UpdateAnimator()
        {
            // Setting variable "velocity" the type "Vector3" -> Grabbing the nav mesh agents velocity
            Vector3 velocity = navMeshAgent.velocity;

            // Setting "localVelocity" to the "velocity" this is meaningfull for the character animation.
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);

            //Adding the "z" speed from "localVelocity" from "velocity"
            float speed = localVelocity.z;

            // getting the Animator component -> grabbing the  "forward_speed" and setting its value to the "speed" from "localVelocity from velocity
            GetComponent<Animator>().SetFloat("forward_speed", speed);
        }
    }
}

