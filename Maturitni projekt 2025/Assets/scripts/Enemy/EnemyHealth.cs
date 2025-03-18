using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject cone;
    [SerializeField] private GameObject glowingShard;

    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0) //pri smrti se vypnou vsechny ostatni komponenty na enemy
        {
            animator.Play("Skeleton@Death01_A", 0, 0f);
            animator.SetTrigger("Death");//nefunguje

            gameObject.GetComponent<EnemyAttack>().StopAttackCoroutine();
            gameObject.GetComponent<EnemyMovement>().enabled = false;
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            gameObject.GetComponent<EnemyAttack>().enabled = false;
            cone.SetActive(false);
            //glowingShard.SetActive(true);
            GetComponentInChildren<GameObject>(glowingShard);
            glowingShard.SetActive(true);
            StartCoroutine(Death());
        }

    }
    private IEnumerator Death()
    {
        yield return new WaitForSeconds(5f);
        gameObject.GetComponent<EnemyHealth>().enabled = false;
    }
}
