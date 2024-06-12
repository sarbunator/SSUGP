using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowfishThreatened : MonoBehaviour
{
    public Transform playerOctopus;

    public float visionDistance;
    public float aggroDistance;
    public bool threatened;

    public Transform eye;
    public Transform iris;
    public float irisMovementRadius;

    public Animator animator;


    
    void Start()
    {
        
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

        if (distanceToPLayer <= aggroDistance)
        {
            //Logistics
            threatened = true;
            animator.SetBool("isAggro", threatened);
        }
        else
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
