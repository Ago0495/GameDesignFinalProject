using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityScript : MonoBehaviour
{
    //variables
    [SerializeField] private protected Rigidbody2D rb2d;
    [SerializeField] private protected int hp;
    [SerializeField] private protected bool canAttack;
    [SerializeField] private protected float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(GameObject obj)
    {

    }

    public void OnCollisionEnter(Collision collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {

        }
    }

    public void OnDefeated()
    {
        Destroy(this.gameObject);
    }
}
