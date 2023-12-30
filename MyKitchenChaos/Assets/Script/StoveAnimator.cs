using UnityEngine;

public class StoveAnimator : MonoBehaviour
{
    [SerializeField] private SotveCounter sotveCounter;
    [SerializeField] private GameObject[] effects ;

    private void Start() {
        sotveCounter.OnStateChanged += SotveCounter_OnStateChanged;
    }

    private void SotveCounter_OnStateChanged(object sender, SotveCounter.EventArgsOnStateChanged e)
    {
        switch (e.state)
        {
            case SotveCounter.State.Idle:
                Hide();
                break;
            case SotveCounter.State.Cooking:
                Show();
                break;
            case SotveCounter.State.Burned:
                Hide();
                break;
        }
    }

    private void Show(){
        foreach (var effect in effects)
        {
            effect.SetActive(true);
        }
    }   

    private void Hide(){
        foreach (var effect in effects)
        {
            effect.SetActive(false);
        }
    }

}
