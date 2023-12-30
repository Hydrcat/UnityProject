using System;
using UnityEngine;

public class CuttingCounter : BaseCounter,IHasProgressBar
{
    // 切菜的次数
    [SerializeField] private MyProgressBar cuttingBar;
    private int cuttingCountNum;
  
    //public event EventHandler OnKitchenObjectPickUp;
    public event EventHandler OnCutting;

    public event EventHandler<IHasProgressBar.EventArgsOnProgressBarChanged> OnProgressBarChanged;

    public override void Interact(Player player)
    {
        // 放下道具
        if (player.GetKitchenObject() != null && GetKitchenObject() == null)
        {
            KitchenObject kitchenObject = player.GetKitchenObject();
            kitchenObject.SetKitchenObjectHolder(this);
            if (kitchenObject.IsSliceable())
            {
                cuttingBar.gameObject.SetActive(true);
                OnProgressBarChanged?.Invoke(this, new IHasProgressBar.EventArgsOnProgressBarChanged
                {
                    progressNormalized = 0f
                });
            }
            return;
        }

        // 取走道具
        if (player.GetKitchenObject() == null && GetKitchenObject() != null)
        {
            cuttingBar.gameObject.SetActive(false);
            //OnKitchenObjectPickUp?.Invoke(this, EventArgs.Empty);
            KitchenObject kitchenObject = GetKitchenObject();
            kitchenObject.SetKitchenObjectHolder(player);
            return;
        }

    }


    public override void InteractAlternate(Player player)
    {
        // 切菜 将可切厨房道具 变成 切后厨房道具
        // 实现功能,玩家可以重复的按下次要交互来增加切菜的进度,进入该状态,无法被取下来.
        if (GetKitchenObject() != null && GetKitchenObject().IsSliceable())
        {
            cuttingCountNum ++ ;
            OnCutting?.Invoke(this,EventArgs.Empty);

            OnProgressBarChanged?.Invoke(this, new IHasProgressBar.EventArgsOnProgressBarChanged()
            {
                progressNormalized = (float)cuttingCountNum / kitchenObject.GetKitchenObjectCuttingCount()
            });
            
            // 如果到达目标次数，则视作被切碎，其实应该还需要管理状态，但这里先这样吧。
            if ( cuttingCountNum >= kitchenObject.GetKitchenObjectCuttingCount() )
            {
                cuttingCountNum = 0;//reset Cutting
                KitchenObject prefab = kitchenObject.GetSlicedObject();
                kitchenObject.DestroySelf();
                KitchenObject.AddKitchenObjectOnHolder(prefab, this);    
            }
            

        }
        
    }
}
