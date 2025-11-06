using System.Collections;
using UnityEngine;

public class TemporaryPlatform : BasePlatform
{
    [SerializeField] private float breakTime = 0.5f;
    
    private bool isBreaking = false;
    private Animator animator;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void SpecialAbility()
    {
        if (!isBreaking)
        {
            animator.SetBool("Break", true);
            StartCoroutine(BreakPlatform());
        }
    }

    private IEnumerator BreakPlatform()
    {
        isBreaking = true;
        yield return new WaitForSeconds(breakTime);
        animator.SetBool("Break", false);
        ClearChildren();
        gameObject.SetActive(false);
        pool.ReturnPlatform(gameObject);
        isBreaking = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            SpecialAbility();
        }
    }
    
}
