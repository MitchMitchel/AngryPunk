using UnityEngine;

public class Puncher : StateMachineBehaviour
{
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PunkScript punk = animator.GetComponent<PunkScript>();

        if (punk != null)
        {
            punk.hasHit = true;
        }
    }

    
}
