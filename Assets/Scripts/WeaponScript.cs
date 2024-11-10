using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class WeaponScript : MonoBehaviour
{
    //variables
    [SerializeField] private Upgrade weaponBaseStats = new Upgrade();
    [SerializeField] private List<Upgrade> weaponUpgrades = new List<Upgrade>();
    private Dictionary<string, int> weaponBaseStatsDict;
    private Collider2D weaponCollider;
    private Animator animator;
    private bool onCooldown;
    private float attackAnimationTime;
    private float cooldownAnimationTime;
    private List<GameObject> alreadyHitList;

    // Start is called before the first frame update
    void Start()
    {
        weaponBaseStatsDict = weaponBaseStats.ToDictionary();
        weaponCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

        alreadyHitList = new List<GameObject>();
        weaponCollider.enabled = false;
        onCooldown = false;

        UpdateAnimClipTimes();
    }

    public virtual void Attack()
    {
        if (!onCooldown)
        {
            float handling = GetTotalStatPower("handling");
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

    public int GetTotalStatPower(string statName)
    {
        return ApplyUpgrades(statName);
    }

    private IEnumerator WeaponCooldown(float waitTime)
    {
        float atkCooldown = GetTotalStatPower("atkCooldown");
        yield return new WaitForSeconds(waitTime);
        weaponCollider.enabled = false;

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
                case "AttackAnimation":
                    attackAnimationTime = clip.length;
                    break;
                case "CooldownAnimation":
                    cooldownAnimationTime = clip.length;
                    break;
            }
        }
    }

    public void AddUpgrade(Upgrade newUpgrade)
    {
        weaponUpgrades.Add(newUpgrade);
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

    public void OnTriggerEnter2D(Collider2D other)
    {
        int atkDamage = (int)GetTotalStatPower("atkDamage");
        float atkKnockback = GetTotalStatPower("atkKnockback");
        
        EntityScript otherEntity = other.gameObject.GetComponent<EntityScript>();

        if (otherEntity != null && !alreadyHitList.Contains(other.gameObject))
        {
            alreadyHitList.Add(other.gameObject);
            Transform thisWeaponParent = transform.parent;

            if (1<<other.gameObject.layer != 1<< thisWeaponParent.gameObject.layer)
            {
                otherEntity.TakeDamage(atkDamage);

                //knockback
                other.attachedRigidbody.AddForce(transform.parent.right * atkKnockback, ForceMode2D.Impulse);
            }
        }
    }
}
