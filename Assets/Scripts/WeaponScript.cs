using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class WeaponScript : MonoBehaviour
{
    //variables
    [SerializeField] private protected int weaponLvl;
    [SerializeField] private protected Upgrade weaponBaseStats = new Upgrade();
    [SerializeField] private protected List<Upgrade> weaponUpgrades = new List<Upgrade>();
    private protected Collider2D weaponCollider;
    private protected Animator animator;
    private protected bool onCooldown;
    private protected float attackAnimationTime;
    private protected float cooldownAnimationTime;
    private protected List<GameObject> alreadyHitList;

    // Start is called before the first frame update
    private protected virtual void Start()
    {
        weaponCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

        alreadyHitList = new List<GameObject>();
        if (weaponCollider != null)
        {
            weaponCollider.enabled = false;
        }
        onCooldown = false;

        if (animator != null)
        {
            UpdateAnimClipTimes();
        }
    }

    public virtual void Attack()
    {
        if (!onCooldown)
        {
            float handling = GetTotalStatPower("handling");
            if (weaponCollider != null)
            {
                weaponCollider.enabled = true;
            }
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

    public int GetTotalStatPower(string statName)
    {
        return ApplyUpgrades(statName);
    }

    private protected IEnumerator WeaponCooldown(float waitTime)
    {
        float atkCooldown = GetTotalStatPower("atkCooldown");
        yield return new WaitForSeconds(waitTime);
        if (weaponCollider != null)
        {
            weaponCollider.enabled = false;
        }

        animator.SetFloat("atkCooldown", 10f / atkCooldown);
        yield return new WaitForSeconds((atkCooldown / 10f) * cooldownAnimationTime);
        onCooldown = false;
    }

    public void UpdateAnimClipTimes()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "AttackAnimation" or "RangedAttackAnimation":
                    attackAnimationTime = clip.length;
                    break;
                case "CooldownAnimation" or "RangedCooldownAnimation":
                    cooldownAnimationTime = clip.length;
                    break;
            }
        }
    }

    public void SetBaseStats(Upgrade upgrade)
    {
        weaponBaseStats = upgrade;
    }

    public Upgrade GetBaseStats()
    {
        return weaponBaseStats;
    }

    public int GetBasePrice()
    {
        return GetBaseStats().GetUpgradeCost();
    }

    public void AddUpgrade(Upgrade newUpgrade)
    {
        weaponUpgrades.Add(newUpgrade);
    }

    public int GetLvl()
    {
        return weaponLvl;
    }

    public int GetNumUpgrades()
    {
        return weaponUpgrades.Count;
    }

    private int ApplyUpgrades(string statName)
    {
        int totalPower = weaponBaseStats.GetStatPower(statName) ;

        foreach (Upgrade upgrade in weaponUpgrades)
        {
            var tempDict = upgrade.ToDictionary();
            if (tempDict.ContainsKey(statName))
            {
                totalPower += tempDict[statName];
            }
        }

        return totalPower;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        int atkDamage = (int)GetTotalStatPower("atkDamage");
        float atkKnockback = GetTotalStatPower("atkKnockback");

        if (other.CompareTag("Hitbox"))
        {
            EntityScript otherEntity = other.gameObject.GetComponentInParent<EntityScript>();

            if (otherEntity != null && !alreadyHitList.Contains(other.gameObject))
            {
                alreadyHitList.Add(other.gameObject);

                if (other.gameObject.layer != gameObject.layer)
                {
                    otherEntity.TakeDamage(atkDamage);
                    ApplyKnockback(other, transform, atkKnockback);
                }
            }
        }
    }

    protected virtual void ApplyKnockback(Collider2D other, Transform weaponTransform, float atkKnockback)
    {
        // Default knockback logic
        other.attachedRigidbody.AddForce(weaponTransform.parent.right * atkKnockback, ForceMode2D.Impulse);
    }
}
