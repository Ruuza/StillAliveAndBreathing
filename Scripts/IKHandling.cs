using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKHandling : MonoBehaviour {

    public Animator anim;
    public Transform LH1;
    public Transform RH1;
    public Transform LH2;
    public Transform RH2;

    public float LeftHandWeight = 1;
    private Transform LeftHandTarget;

    public float RightHandWeight = 1;
    private Transform RightHandTarget;

 //   public Transform weapon;
 //   public Vector3 lookPos;

	void Start () {
        LeftHandTarget = LH1;
        RightHandTarget = RH1;
	}
	
	void Update () {
		
	}
    private void OnAnimatorIK(int layerIndex)
    {

        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, LeftHandWeight);
        anim.SetIKPosition(AvatarIKGoal.LeftHand, LeftHandTarget.position);
        anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, LeftHandWeight);
        anim.SetIKRotation(AvatarIKGoal.LeftHand, LeftHandTarget.rotation);

        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, RightHandWeight);
        anim.SetIKPosition(AvatarIKGoal.RightHand, RightHandTarget.position);
        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, RightHandWeight);
        anim.SetIKRotation(AvatarIKGoal.RightHand, RightHandTarget.rotation);
    }

    public void ChangeWeaponIK(bool pistol)
    {
        if (pistol)
        {
            LeftHandTarget = LH1;
            RightHandTarget = RH1;
        }
        else
        {
            LeftHandTarget = LH2;
            RightHandTarget = RH2;
        }
    }

}
