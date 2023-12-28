using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private GameObject[] uiObjects;
    [SerializeField] private CuttingCounter cuttingCounter;

    private void Start()
    {
        cuttingCounter.OnProgeressBarChanged += CuttingCounter_OnProgeressBarChanged;
        cuttingCounter.OnKitchenObjectPickUp += CuttingCounter_OnKitchenObjectPickUp;

    }

    private void CuttingCounter_OnKitchenObjectPickUp(object sender, EventArgs e)
    {
        Hide();
    }


    // 备注：EventHandle可以指定传入消息<TEventArgs>从而告知IDE，以方便自动补全。
    private void CuttingCounter_OnProgeressBarChanged(object sender, CuttingCounter.EventArgsOnProgressBarChanged e)
    {
        // 检查是否隐藏，首先显示
        Show();
        barImage.fillAmount = e.progressNormalized;

    }


    // 隐藏
    private void Hide()
    {
        foreach (var obj in uiObjects)
        {
            obj.SetActive(false);
        }
    }
    // 展示
    private void Show() 
    {
        foreach(var obj in uiObjects)
        {
            obj.SetActive(true);
        }
    }
    
}
