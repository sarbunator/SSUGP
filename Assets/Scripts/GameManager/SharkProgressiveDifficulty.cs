using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkProgressiveDifficulty : MonoBehaviour
{

    public float difficultyTimer;
    public float sharkDamage;
    public float sharkSpeed;
    public float aggroRange;
    [HideInInspector]
    public int rotationTimes = 0;
    private bool waiting;



    IEnumerator AddedDifficultyTimer()
    {
        waiting = true;
        yield return new WaitForSecondsRealtime(difficultyTimer);
        rotationTimes +=1;
        sharkDamage += 1;
        waiting = false;
        if (rotationTimes / 4 >= 1) 
        {
            sharkSpeed += 1;
        }
        if (rotationTimes / 8 >= 1)
        {
            aggroRange += 1;
        }



    }

    private void FixedUpdate()
    {
        if (waiting)
            return;
        StartCoroutine(AddedDifficultyTimer());
    }
}
