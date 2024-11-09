using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    //variables
    [SerializeField] private int atkDamage;
    [SerializeField] private float atkRange;
    [SerializeField] private float atkKnockbackForce;
    [SerializeField] private float atkCooldown;
    [SerializeField] private float handling;
    //[SerializeField] private int weaponTag;
    //[SerializeField] private int upgrade;
    private Collider2D weaponCollider;
    private Animator animator;
    private bool onCooldown;
    private float attackAnimationTime;
    private float cooldownAnimationTime;
    private List<GameObject> alreadyHitList;
    private Color col;

    // Start is called before the first frame update
    void Start()
    {
        weaponCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

        alreadyHitList = new List<GameObject>();
        weaponCollider.enabled = false;
        onCooldown = false;
        col = GetComponent<Renderer>().material.color;

        UpdateAnimClipTimes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Attack()
    {
        if (!onCooldown)
        {
            weaponCollider.enabled = true;
            alreadyHitList.Clear();

            //start coroutine to turn off collider
            StartCoroutine(WeaponCooldown((10f / handling) * attackAnimationTime));
            onCooldown = true;

            //play swing animation
            animator.SetFloat("atkSpeed", (handling / 10f));
            //animator.SetBool("attack", true);
            animator.Play("AttackAnimation");
        }
    }

    public int getAtkDamage()
    {
        return atkDamage;
    }

    public float GetAtkRange()
    { 
        return atkRange; 
    }

    public float GetAtkCooldown() 
    {  
        return atkCooldown;
    }
    public float GetHandling()
    {
        return handling;
    }

    private IEnumerator WeaponCooldown(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        weaponCollider.enabled = false;

        animator.SetFloat("atkCooldown", 1f / atkCooldown);
        yield return new WaitForSeconds((atkCooldown / 1f) * cooldownAnimationTime);
        onCooldown = false;
    }

    public void UpdateAnimClipTimes()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "AttackAnimation":
                    attackAnimationTime = clip.length;
                    break;
                case "CooldownAnimation":
                    cooldownAnimationTime = clip.length;
                    break;
            }
        }
    }

        public void OnTriggerEnter2D(Collider2D other)
    {
        EntityScript otherEntity = other.gameObject.GetComponent<EntityScript>();

        if (otherEntity != null && !alreadyHitList.Contains(other.gameObject))
        {
            alreadyHitList.Add(other.gameObject);
            Transform thisWeaponParent = transform.parent;

            if (1<<other.gameObject.layer != 1<< thisWeaponParent.gameObject.layer)
            {
                otherEntity.TakeDamage(atkDamage);

                //knockback
                other.attachedRigidbody.AddForce(transform.parent.right * atkKnockbackForce, ForceMode2D.Impulse);
            }
        }
    }
}
