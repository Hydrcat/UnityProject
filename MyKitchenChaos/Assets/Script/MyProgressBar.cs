using System;
using UnityEngine;
using UnityEngine.UI;

public class MyProgressBar : MonoBehaviour
{
    [SerializeField] private Image barImage;
   // [SerializeField] private GameObject[] uiObjects;
    [SerializeField] private GameObject objectHasBar;

    private void Start()
    {
        IHasProgressBar hasProgressBar = objectHasBar.GetComponent<IHasProgressBar>();
        hasProgressBar.OnProgressBarChanged += counterHasBar_OnProgressBarchanged ;
    }

    // 备注：EventHandle可以指定传入消息<TEventArgs>从而告知IDE，以方便自动补全。
    private void counterHasBar_OnProgressBarchanged(object sender, IHasProgressBar.EventArgsOnProgressBarChanged e)
    {
        // 检查是否隐藏，首先显示
        barImage.fillAmount = e.progressNormalized;
    }


    // 隐藏
    // private void Hide()
    // {
    //     foreach (var obj in uiObjects)
    //     {
    //         obj.SetActive(false);
    //     }
    // }
    // // 展示
    // private void Show() 
    // {
    //     foreach(var obj in uiObjects)
    //     {
    //         obj.SetActive(true);
    //     }
    // }
    
}
