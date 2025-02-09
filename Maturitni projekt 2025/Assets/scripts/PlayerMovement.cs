using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private LayerMask collidibleObjects;
    [SerializeField] private Animator animator;
    [SerializeField] float rotateSpeed;
    [SerializeField] float playerSize;

    void Update()
    {
        Vector3 moveDir = new Vector3();

        bool pica = Input.GetKey(KeyCode.Space);

        if (Input.GetKey(KeyCode.W)) { moveDir.z = 1f; }
        if (Input.GetKey(KeyCode.S)) { moveDir.z = -1f; }
        if (Input.GetKey(KeyCode.A)) { moveDir.x = -1f; }
        if (Input.GetKey(KeyCode.D)) { moveDir.x = 1f; }
        moveDir.Normalize();

        //Vytvoreni neviditelneho boxu pro kontrolu kolize (box ma velikost projistotu o kousek vetsi, nez hrac)
        float moveDistance = movementSpeed * Time.deltaTime;
        bool canMove = !Physics.BoxCast(transform.position, Vector3.one * playerSize, moveDir, Quaternion.identity, moveDistance, collidibleObjects);   //vrati true kdiz v se v prostoru boxu nenachazi prekazka

        if (!canMove) //kdyz v ceste stoji prekazka, tak se zkusi rozlozit vektory tak, aby sel tam, kde neni prekazka..........
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = !Physics.BoxCast(transform.position, Vector3.one * playerSize, moveDirX, Quaternion.identity, moveDistance, collidibleObjects);//TOHLETO!!!!!!!!!!!!!!!!!!!!

            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                canMove = !Physics.BoxCast(transform.position, Vector3.one * playerSize, moveDirZ, Quaternion.identity, moveDistance, collidibleObjects);//TOHLETO!!!!!!!!!!!!!!!!!!!!

                if (canMove)
                {
                    moveDir = moveDirZ;
                }
            }
        }

        if (canMove) //kdyz v ceste nestoji prekazka, tak se hrac posune
        {
            Vector3 pos = transform.position;
            transform.position += moveDir * moveDistance;
        }

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed); //hrac se pohybuje a otaci do smeru chuze.......

        float moveDirAbsoluteValue = Mathf.Abs(moveDir.x) + Mathf.Abs(moveDir.y) + Mathf.Abs(moveDir.z);
        if (moveDirAbsoluteValue != 0)                                                    //kdyz absolutni hodnota vektoru nenolova hraje animace
        {
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }
}
//    private void HandleMovement()
//    {

//        Vector3 moveDir = new Vector3();

//        bool pica = Input.GetKey(KeyCode.Space);

//        if (Input.GetKey(KeyCode.W)) { moveDir.z = 1f; }
//        if (Input.GetKey(KeyCode.S)) { moveDir.z = -1f; }
//        if (Input.GetKey(KeyCode.A)) { moveDir.x = -1f; }
//        if (Input.GetKey(KeyCode.D)) { moveDir.x = 1f; }
//        moveDir.Normalize();

//        //Vytvoreni neviditelneho boxu pro kontrolu kolize (box ma velikost projistotu o kousek vetsi, nez hrac)
//        float moveDistance = movementSpeed * Time.deltaTime;
//        float playerSize = .25f;
//        bool canMove = !Physics.BoxCast(transform.position, Vector3.one * playerSize, moveDir, Quaternion.identity, moveDistance, collidibleObjects);   //vrati true kdiz v se v prostoru boxu nenachazi prekazka

//        if (!canMove) //kdyz v ceste stoji prekazka, tak se zkusi rozlozit vektory tak, aby sel tam, kde neni prekazka..........
//        {
//            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
//            canMove = !Physics.BoxCast(transform.position, Vector3.one * playerSize, moveDirX, Quaternion.identity, moveDistance, collidibleObjects);//TOHLETO!!!!!!!!!!!!!!!!!!!!

//            if (canMove)
//            {
//                moveDir = moveDirX;
//            }
//            else
//            {
//                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
//                canMove = !Physics.BoxCast(transform.position, Vector3.one * playerSize, moveDirZ, Quaternion.identity, moveDistance, collidibleObjects);//TOHLETO!!!!!!!!!!!!!!!!!!!!

//                if (canMove)
//                {
//                    moveDir = moveDirZ;
//                }
//            }
//        }

//        if (canMove) //kdyz v ceste nestoji prekazka, tak se hrac posune
//        {
//            Vector3 pos = transform.position;
//            transform.position += moveDir * moveDistance;
//        }

//        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed); //hrac se pohybuje a otaci do smeru chuze.......

//        float moveDirAbsoluteValue = Mathf.Abs(moveDir.x) + Mathf.Abs(moveDir.y) + Mathf.Abs(moveDir.z); 
//        if (moveDirAbsoluteValue != 0)                                                    //kdyz absolutni hodnota vektoru nenolova hraje animace
//        {
//            animator.SetBool("IsMoving", true);
//        }
//        else
//        {
//            animator.SetBool("IsMoving", false);
//        }

//    }






