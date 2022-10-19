using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBulletController : MonoBehaviour
{
    [SerializeField]
    float speed = 10f;

    [SerializeField]
    float damage = 10f;

    [SerializeField]
    float lifeTime = 5f;

    [SerializeField] 
    LayerMask whatCanBeDestroy;

    Rigidbody rb;
    float currentTime = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        rb.velocity = transform.forward * speed;
        currentTime = Time.deltaTime;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (whatCanBeDestroy  == (whatCanBeDestroy | (1 << collider.gameObject.layer))){
            //Destroy(collider.gameObject);
            
            HealthController healthController = collider.gameObject.GetComponent<HealthController>();
            if (healthController)
            {
                healthController.makeDamage(damage);
            }
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
