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
        
        if (currentHealth <= 0) 
        {
            Instantiate(deadBody, transform.position, Quaternion.identity);  //vytvo�� pr�zdn�ho nep��tele         
            StartCoroutine(Death());
        }

    }
    private IEnumerator Death() //po�k� na konec sn�mku
    {
        Instantiate(glowingShard, transform.position, Quaternion.identity); //ud�l� shard

        yield return new WaitForEndOfFrame();

        cone.SetActive(false);                                      //vyp�n� se tu pro jistotu v�echno
        gameObject.GetComponent<EnemyMovement>().enabled = false;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        gameObject.GetComponent<EnemyAttack>().enabled = false;
        gameObject.GetComponent<EnemyHealth>().enabled = false;
        Destroy(gameObject);
    }
}
