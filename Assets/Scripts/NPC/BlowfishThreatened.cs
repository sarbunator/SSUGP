using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowfishThreatened : MonoBehaviour
{
    public Transform playerOctopus;

    public float visionDistance;
    public float aggroDistance;
    public bool threatened;
    public float reactionTime;

    public Transform eye;
    public Transform iris;
    public float irisMovementRadius;

    public Animator animator;


    
    void Start()
    {
        
    }

    IEnumerator PufferfishReaction()
    {
        threatened = true;
        yield return new WaitForSecondsRealtime(reactionTime);
        
        animator.SetBool("isAggro", threatened);

    }

    public void BlowFishEyeFollow()
    {
        float visionDistanceToPLayer = Vector3.Distance(transform.position, playerOctopus.position);

        if (visionDistanceToPLayer <= visionDistance)
        {

        }
        else
        {

        }
    }
    
    public void BlowFishAggro()
    {
        float distanceToPLayer = Vector3.Distance(transform.position, playerOctopus.position);

        if (distanceToPLayer <= aggroDistance && threatened == false)
        {
            StartCoroutine(PufferfishReaction());
        }
        else if (distanceToPLayer > aggroDistance && threatened == true)
        {
            threatened = false;
            animator.SetBool("isAggro", threatened);
        }
    }

    void Update()
    {

        BlowFishAggro();

    }




}
