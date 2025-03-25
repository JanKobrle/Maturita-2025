using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject cone;
    [SerializeField] private GameObject glowingShard;
    [SerializeField] private GameObject deadBody;

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
            //animator.SetTrigger("Death");//nefunguje
            Instantiate(deadBody, transform.position, Quaternion.identity);  //hodi tam prazdnyho enemaka s animaci smrti          
            StartCoroutine(Death());
        }

    }
    private IEnumerator Death()
    {
        Instantiate(glowingShard, transform.position, Quaternion.identity);

        yield return new WaitForEndOfFrame();
        gameObject.GetComponent<EnemyMovement>().enabled = false;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        gameObject.GetComponent<EnemyAttack>().enabled = false;
        cone.SetActive(false);

        gameObject.GetComponent<EnemyHealth>().enabled = false;
        Destroy(gameObject);
    }
}
