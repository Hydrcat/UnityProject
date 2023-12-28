using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    // 切菜的次数
    [SerializeField] private Canvas cuttingBar;
    private int cuttingCountNum;
    public event EventHandler<EventArgsOnProgressBarChanged> OnProgeressBarChanged;
    public class EventArgsOnProgressBarChanged : EventArgs
    {
        public float progressNormalized; // 注意到一个细节，如果public类里面不使用的public字段，不会触发未使用提醒。
    }

    public event EventHandler OnKitchenObjectPickUp;
    public event EventHandler OnCutting;
    public override void Interact(Player player)
    {
        // 放下道具
        if (player.GetKitchenObject() != null && GetKitchenObject() == null)
        {
            KitchenObject kitchenObject = player.GetKitchenObject();
            kitchenObject.SetKitchenObjectHolder(this);
            if (kitchenObject.IsSliceable())
            {
                OnProgeressBarChanged?.Invoke(this, new EventArgsOnProgressBarChanged
                {
                    progressNormalized = 0f
                });
            }
            return;
        }

        // 取走道具
        if (player.GetKitchenObject() == null && GetKitchenObject() != null)
        {
            OnKitchenObjectPickUp?.Invoke(this, EventArgs.Empty);
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

            OnProgeressBarChanged?.Invoke(this, new EventArgsOnProgressBarChanged()
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
