using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.FirstPerson
{
    public class SpaceManAnimatorScript : MonoBehaviour
    {
        Animator animator;

        // Use this for initialization
        void Start()
        {
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            bool idle = true;
            if (Input.GetAxis("Horizontal") > 0f)
            {
                idle = false;
                animator.SetBool("walk_left", !idle);
                animator.SetBool("walk_right", idle);
            }
            else if (Input.GetAxis("Horizontal") < 0f)
            {
                idle = false;
                animator.SetBool("walk_right", !idle);
                animator.SetBool("walk_left", idle);
            }
            if (Input.GetAxis("Vertical") > 0f)
            {
                idle = false;
                animator.SetBool("walk_foward", !idle);
                animator.SetBool("walk_back", idle);
            }
            else if (Input.GetAxis("Vertical") < 0f)
            {
                idle = false;
                animator.SetBool("walk_back", !idle);
                animator.SetBool("walk_foward", idle);
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetFloat("speed", 2);
                idle = false;
            }
            else
            {
                animator.SetFloat("speed", 1);
            }
            if (idle)
            {
                Debug.Log(Input.GetAxis("Horizontal"));
                Debug.Log(Input.GetAxis("Vertical"));
                animator.SetFloat("speed", 0);
                animator.SetBool("walk_back", !idle);
                animator.SetBool("walk_foward", !idle);
                animator.SetBool("walk_right", !idle);
                animator.SetBool("walk_left", !idle);
            }
            animator.SetBool("idle", idle);
            // float h = Input.GetButtonDown("Jump");
            // float v = Input.GetAxis("Vertical");
            // bool fire = Input.GetButtonDown("Fire1");

            // animator.SetFloat("Forward", v);
            // animator.SetFloat("Strafe", h);
            // animator.SetBool("Fire", fire);
        }

        void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                animator.SetTrigger("Die");
            }
        }
    }
}