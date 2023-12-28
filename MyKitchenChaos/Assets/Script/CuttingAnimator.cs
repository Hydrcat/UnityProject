using System;
using UnityEngine;

public class CuttingAnimator : MonoBehaviour
{
    const string TRIGGER_CUT = "Cut";

    [SerializeField] private CuttingCounter counter;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        counter.OnCutting += Counter_OnCutting;
        
    }

    private void Counter_OnCutting(object sender, EventArgs e)
    {
        animator.SetTrigger(TRIGGER_CUT);
    }

}
