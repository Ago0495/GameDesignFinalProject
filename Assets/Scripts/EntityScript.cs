using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityScript : MonoBehaviour
{
    //variables
    public int Hp;
    public bool CanAtk;
    public float MovSpeed;
    private Rigidbody myRig;

    // Start is called before the first frame update
    void Start()
    {
        myRig = GetComponent<Rigidbody>();
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
