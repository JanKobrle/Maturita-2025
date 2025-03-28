using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAtack : MonoBehaviour
{
    [SerializeField] private Joystick attackJoystick;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private Image zoneCircle;
    [SerializeField] private Color chargedColor;
    [SerializeField] private Color notChargedColor;
    [SerializeField] private PlayerWeapon weapon;
    [SerializeField] private Animator animator;
    private PlayerMovement playerMovement;

    private bool charged = false;
    private Vector3 tempAttackDir;
    private float nextTimeToAtack = 0f;

    void Start()
    {
        weapon = GetComponent<PlayerWeapon>();  
        playerMovement = GetComponent<PlayerMovement>();

        //ud�l� kole�ko pod hr��em, ze kter�ho se pak vybere v�se� na �tok
        zoneCircle.transform.parent.gameObject.SetActive(true); 
        zoneCircle.fillAmount = weapon.angle * 2f / 360f;
        zoneCircle.rectTransform.localScale *= weapon.range * 2;
        zoneCircle.transform.parent.gameObject.SetActive(false);
    }

    void Update()
    {
        Vector3 attackDir = new Vector3(attackJoystick.Horizontal, 0, attackJoystick.Vertical).normalized;   //Joystick input

        if (attackDir != Vector3.zero)      //kdyz se dotkne joysticku
        {
            charged = true;
            tempAttackDir = attackDir;
            DrawZoneCone(true);
        }
        if (charged && attackDir == Vector3.zero)           //kdyz pusti joystick
        {
            DrawZoneCone(false);
            Attack(tempAttackDir);
            charged = false;
        }
        if (Time.time <= nextTimeToAtack) { zoneCircle.color = notChargedColor; } else { zoneCircle.color = chargedColor; }
    }
     

    private void Attack(Vector3 attackDir)
    {
        if (Time.time <= nextTimeToAtack) { return; }       
        nextTimeToAtack = Time.time + weapon.duration; 

        playerMovement.MovementIntervention(attackDir, 0.24f); //po�k� a� prob�hne animace �ktoku

        animator.SetTrigger("Attack");  

        List<Transform> toHitTransforms = new List<Transform>();
        Collider[] enemies = Physics.OverlapSphere(transform.position, weapon.range, enemyMask); // najde v�echny v dosahu

        foreach (Collider e in enemies) //zkontroluje, zda jsou ve v�se�i
        {

            if (Vector3.Angle(attackDir, e.transform.position - transform.position) <= weapon.angle)
            {
                toHitTransforms.Add(e.transform);
            }
        }
        foreach (Transform t in toHitTransforms) // ubere �ivoty v�em zasa�en�m
        {
            t.GetComponent<EnemyHealth>().TakeDamage(weapon.damage);
        }
    }
    private void DrawZoneCone(bool set) //nakresl� kde v�ude �tok zas�hne
    {
        if (set)
        {
            zoneCircle.transform.parent.gameObject.SetActive(true);
            //n�sleduj�c� jeden ��dek byl naps�n s pomoc� ChatGPT
            zoneCircle.rectTransform.eulerAngles = new Vector3(90, 0, Mathf.Atan2(attackJoystick.Vertical, attackJoystick.Horizontal) / Mathf.PI * 180f - weapon.angle);
        }
        else
        {
            zoneCircle.rectTransform.eulerAngles = Vector3.zero;
            zoneCircle.transform.parent.gameObject.SetActive(false);
        }
    }

    //naktersl� atackrange v editoru
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, weapon.range);
    }
}
