using UnityEngine;

public class HealthController : MonoBehaviour
{

    [SerializeField]
    float health = 100;

    /*
    [SerializeField]
    bool _makedamage;*/

    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetFloat("Health", health);
        //if (_makedamage)
        //{
        //    makeDamage(20);
        //    _makedamage = false;
        //}
    }

    public void makeDamage(float damage)
    {
        health -= damage;
        animator.SetTrigger("Damage");
    }

    public float getHealth()
    {
        return health;
    }
    
    public bool IsDie()
    {
        return health <= 0;
    }
}
