using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObejctSO;
    public event EventHandler OnOpenContainerCounter;

    // 容器桌台 能进行的交互 只有 
    // 1.卓台为空的时候取出道具放在台上
    // 2.桌台上有道具并且玩家无道具的时候取走
    public override void Interact(Player player)
    {

        //if (this.GetKitchenObject() != null && player.GetKitchenObject()==null) 
        //{
        //    GetKitchenObject().SetKitchenObjectHolder(player);
        //    return;
        //}
        
        /**** 如果玩家手里没有道具，则打开箱子给一个道具*****/
        if (player.GetKitchenObject() == null)
        {
            KitchenObject.AddKitchenObjectOnHolder(kitchenObejctSO.prefab, player);
            OnOpenContainerCounter?.Invoke(this, EventArgs.Empty);
            return;
        }
    }


}
