using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollScript : MonoBehaviour
{
    [SerializeField] Transform[] target;
    [SerializeField] float arriveRadius;
    [SerializeField] float animationTime;
    SkeletonScript character;
    int idx, count;
    bool wait = false, patrolling = false;
    Coroutine playingAnimation;
    private void Start()
    {
        character = GetComponent<SkeletonScript>();
        count = target.Length;
    }
    private void Update()
    {
        if(patrolling && !wait)
        {
            Arrive(target[idx].position);
        }
    }
    // public functions
    public void Patroll()
    {
        if(count > 0)
        {
            if(patrolling == false)
            {
                WalkToward(target[idx].position);
                patrolling = true;
            }
        }
    }
    public void StopPatroll()
    {
        StopCoroutine(playingAnimation);
        wait = false;
        patrolling = false;
    }
    // private functions
    void WalkToward(Vector3 position)
    {
        character.FaceDirection(position);
        character.Walk();
    }
    void Arrive(Vector3 position)
    {
        if (Vector3.Distance(transform.position, position) < arriveRadius)
        {
            if (++idx == count)
                idx = 0;
            playingAnimation = StartCoroutine(LookAroundAnimation());
        }
    }
    IEnumerator LookAroundAnimation()
    {
        character.Stop();
        wait = true;
        character.PlayAnimation("LookAround");
        yield return new WaitForSeconds(animationTime);
        wait = false;
        patrolling = false;
    }
}
