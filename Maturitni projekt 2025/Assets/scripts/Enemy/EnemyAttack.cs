using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OD.Player;


namespace OD.Dungeon
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private float damage;
        [SerializeField] private float hitRange;
        [SerializeField] private float hitAngle;
        [SerializeField] private float hitLoadTime;
        [SerializeField] private Image zoneCircle;
        [SerializeField] private Image animatedZoneCircle;
        [SerializeField] private Animator animator;

        private bool canAttack = true;
        private bool rotate = true;
        private Transform targett;
        private bool animateCone = false;
        private float t = 0f;
        private Vector3 startScale;

        private Coroutine cor = null;
        private void Start()
        {
            zoneCircle.transform.parent.gameObject.SetActive(false);
        }
        private void Update()
        {

            if (rotate && targett != null)
            {
                transform.forward = Vector3.Slerp(transform.forward, targett.position - transform.position, Time.deltaTime * 10f);
                if (Vector3.Angle(targett.position - transform.position, transform.forward) < 5f)
                {
                    rotate = false;
                }
            }
            if (animateCone)
            {
                animatedZoneCircle.rectTransform.localScale = Vector3.Lerp(startScale, startScale * hitRange * 2, t);
                t += Time.deltaTime / hitLoadTime;
            }
        }
        public void CancelRotate()
        {
            rotate = false;
        }
        public void StartAttack(Transform target)
        {

            if (canAttack)
            {
                if (!rotate && Vector3.Angle(target.position - transform.position, transform.forward) < 5f)
                {
                    cor = StartCoroutine(AttackHit(target));
                }
                else
                {
                    targett = target;
                    rotate = true;
                }
            }
        }
        private IEnumerator AttackHit(Transform target)                                                                                                                         //[20]
        {
            animator.SetTrigger("Attack");
            canAttack = false;
            GetComponent<EnemyMovement>().DisableMovement();
            DrawZoneCone(true);
            yield return new WaitForSeconds(hitLoadTime); //tak aby sedìlo s animací
            if (Vector3.Angle(target.position - transform.position, transform.forward)
                <= hitAngle && Vector3.Distance(transform.position, target.position) <= hitRange)
            {
                target.GetComponent<PlayerHealth>().TakeDamage(damage);
            }
            DrawZoneCone(false);
            GetComponent<EnemyMovement>().EnableMovement();
            canAttack = true;
        }

        public void StopAttackCoroutine()
        {
            StopCoroutine(cor);
        }
        private void DrawZoneCone(bool set)
        {
            if (set)
            {
                zoneCircle.transform.parent.gameObject.SetActive(true);
                zoneCircle.rectTransform.Rotate(0, 0, hitAngle);
                zoneCircle.fillAmount = hitAngle * 2f / 360f;
                zoneCircle.rectTransform.localScale *= hitRange * 2;

                animatedZoneCircle.fillAmount = hitAngle * 2f / 360f;
                animatedZoneCircle.rectTransform.Rotate(0, 0, hitAngle);

                startScale = animatedZoneCircle.rectTransform.localScale;
                animateCone = true;
            }
            else
            {
                animateCone = false;
                startScale = Vector3.zero;
                t = 0f;

                animatedZoneCircle.rectTransform.localScale = Vector3.one;
                animatedZoneCircle.fillAmount = 1f;
                animatedZoneCircle.rectTransform.Rotate(0, 0, -hitAngle);

                zoneCircle.rectTransform.localScale = Vector3.one;
                zoneCircle.fillAmount = 1f;
                zoneCircle.rectTransform.Rotate(0, 0, -hitAngle);
                zoneCircle.transform.parent.gameObject.SetActive(false);

            }
        }
        //v editoru nakdeslí atack range
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, hitRange);
        }
    }
}
