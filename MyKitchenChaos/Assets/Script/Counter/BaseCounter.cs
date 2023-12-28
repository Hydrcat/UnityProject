using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour,IKichenHolder
{
    // 所有柜台的基类


    [SerializeField] protected GameObject KitchenObjectHolder;

    protected KitchenObject kitchenObject;
   

    public virtual void Interact(Player player)
    {
        //此基类的虚方法不应该被直接访问
        Debug.LogError("BaseCounter.Interact() 不能被直接访问");
    }

    public virtual void InteractAlternate(Player player)
    {
        //此基类的虚方法不应该被直接访问
        Debug.LogError("BaseCounter.InteractAlternate() 不能被直接访问");
    }


    // 厨房道具持有接口实现
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void SetKitchenObject(KitchenObject kObject)
    {
        if (kObject == null)
        {
            throw new ArgumentNullException(nameof(kObject));
        }
        kitchenObject = kObject;
    }

    public GameObject GetKitchenObjectHolder()
    {
        return KitchenObjectHolder;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

}
