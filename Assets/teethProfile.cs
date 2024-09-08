using UnityEngine;
using System.Collections;
using System;

public class teethProfile : MonoBehaviour
{
    private Animator animator;
    public float stepDuration = 0.1f; // Duration to play the animation
    public float healTeethDuration = 5f;
    public float reverseSpeedFactor = 0.1f;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = 0;
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.E))
        // {
        //     animator.Play("tooth");
        //     animator.speed = 1;
        // }
        // if (Input.GetKeyDown(KeyCode.D))
        // {
        //     StartCoroutine(StepBack());
        // }
    }

    public void recieveDamage()
    {
        StartCoroutine(StepForward());
    }

    public void healTeeth()
    {
        StartCoroutine(StepBack());

    }

    public IEnumerator StepForward()
    {
        animator.speed = 1;
        yield return new WaitForSeconds(stepDuration);
        animator.speed = 0;
    }
    public IEnumerator StepBack()
    {
        float elapsedTime = 0f;

        while (elapsedTime < stepDuration)
        {
            elapsedTime += Time.deltaTime * reverseSpeedFactor;
            float normalizedTime = Mathf.Lerp(1f, 0f, elapsedTime / stepDuration);
            animator.Play(animator.GetCurrentAnimatorStateInfo(0).shortNameHash, -1, normalizedTime);
            yield return null;
        }

        animator.speed = 0;
    }

}
