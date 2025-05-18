using System.Collections;
using UnityEngine;

namespace OD.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;
        [SerializeField] private LayerMask collidibleObjects;
        [SerializeField] private Animator animator;
        [SerializeField] float rotateSpeed;
        [SerializeField] float playerSize;
        [SerializeField] private Joystick movementJoystick;
        private bool frozen;
        private Vector3 atackDir;

        public void MovementIntervention(Vector3 attackDir, float time)
        {
            StartCoroutine(WaitForSeconds(time));
            atackDir = attackDir;
        }
        IEnumerator WaitForSeconds(float seconds)
        {
            frozen = true;
            yield return new WaitForSeconds(seconds);
            frozen = false;
        }
        void Update()
        {
            Vector3 moveDir = new Vector3(movementJoystick.Horizontal, 0, movementJoystick.Vertical);   //Joystick input

            if (moveDir.x == 0 && moveDir.z == 0)      //pro testovani na klavesnici (jen kdyz input = null)
            {
                if (Input.GetKey(KeyCode.W)) { moveDir.z = 1f; }
                if (Input.GetKey(KeyCode.S)) { moveDir.z = -1f; }
                if (Input.GetKey(KeyCode.A)) { moveDir.x = -1f; }
                if (Input.GetKey(KeyCode.D)) { moveDir.x = 1f; }
                moveDir.Normalize();                  //aby byl diagonalni pohyb roven 1
            }

            //Vytvoreni neviditelneho boxu pro kontrolu kolize (box ma velikost projistotu o kousek vetsi, nez hrac)
            float moveDistance = movementSpeed * Time.deltaTime;
            bool canMove = !Physics.BoxCast(transform.position, Vector3.one * playerSize, moveDir, Quaternion.identity, moveDistance, collidibleObjects);   //vrati true kdiz v se v prostoru boxu nenachazi prekazka

            if (!canMove) //kdyz v ceste stoji prekazka, tak se zkusi rozlozit vektory tak, aby sel pouzil jen ten, kde neni prekazka
            {
                Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
                canMove = !Physics.BoxCast(transform.position, Vector3.one * playerSize, moveDirX, Quaternion.identity, moveDistance, collidibleObjects);//...

                if (canMove)
                {
                    moveDir = moveDirX / 2; //hrac bezi polovicni rychlosti kdyz bezi do zdi
                }
                else
                {
                    Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                    canMove = !Physics.BoxCast(transform.position, Vector3.one * playerSize, moveDirZ, Quaternion.identity, moveDistance, collidibleObjects);//...

                    if (canMove)
                    {
                        moveDir = moveDirZ / 2; // -||-
                    }
                }
            }

            if (canMove && !frozen) //kdyz v ceste nestoji prekazka, tak se hrac posune
            {
                Vector3 pos = transform.position;
                transform.position += moveDir * moveDistance;
            }
            if (!frozen)
            {
                transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed); //hrac se pohybuje a otaci do smeru chuze.......
            }
            else
            {
                transform.forward = Vector3.Slerp(transform.forward, atackDir, Time.deltaTime * 30f);
            }
            float moveDirAbsoluteValue = Mathf.Abs(moveDir.x) + Mathf.Abs(moveDir.y) + Mathf.Abs(moveDir.z);
            if (moveDirAbsoluteValue != 0)               //kdyz absolutni hodnota vektoru nenolova hraje animace
            {
                animator.SetBool("IsMoving", true);
            }
            else
            {
                animator.SetBool("IsMoving", false);
            }
        }
    }
}






