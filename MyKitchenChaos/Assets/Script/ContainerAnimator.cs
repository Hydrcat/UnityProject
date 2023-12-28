using UnityEngine;

public class ContainerAnimator : MonoBehaviour
{
    const string TRIGGER_OPEN = "Open";

    [SerializeField] private ContainerCounter counter;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        counter.OnOpenContainerCounter += Counter_OnOpenContainerCounter;
        
    }

    private void Counter_OnOpenContainerCounter(object sender, System.EventArgs e)
    {
        animator.SetTrigger(TRIGGER_OPEN);
    }
}
