using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class toggletest : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(1, "ATTACK_BASIC_1", false);
        else if (Input.GetMouseButtonDown(1))
            GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(1, "SHIELD_BASIC_1", false);
        else
            GetComponent<SkeletonAnimation>().AnimationState.AddAnimation(0, "IDLE", true, 0);
    }
}
