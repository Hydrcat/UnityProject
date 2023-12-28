using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    // 获取当前厨房道具的配置信息
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKichenHolder kichenHolder;
    public KitchenObjectSO GetKitchenObjectSO() { return kitchenObjectSO; }

    public void SetKitchenObjectHolder(IKichenHolder kichenHolder)
    {
        if (this.kichenHolder != null)
        {
            this.kichenHolder.ClearKitchenObject();
        }
        this.kichenHolder = kichenHolder;

        if (kichenHolder.HasKitchenObject())
        {
            Debug.LogError("Counter Already has a KitchenObject!");
        }

        kichenHolder.SetKitchenObject(this);
        transform.parent = kichenHolder.GetKitchenObjectHolder().transform;
        transform.localPosition = Vector3.zero;

    }
    
    public void DestroySelf()
    {
        kichenHolder.ClearKitchenObject();
        Destroy(gameObject);
    }

    public static KitchenObject AddKitchenObjectOnHolder(KitchenObject prefab,IKichenHolder holder)
    {
        // 静态方法，创建厨房道具的实例，然后利用挂载到对应的接口上
        KitchenObject kitchenObject =  Instantiate(prefab,holder.GetKitchenObjectHolder().transform);
        kitchenObject.SetKitchenObjectHolder(holder);
        return kitchenObject;
    }
    public IKichenHolder GetKichenObjectHolder() { return kichenHolder; }
    public bool IsSliceable() { return kitchenObjectSO.SliceObject != null; }
    public KitchenObject GetSlicedObject() { return kitchenObjectSO.SliceObject; }
    public int GetKitchenObjectCuttingCount() { return kitchenObjectSO.TargetCuttinCountNum;}

}
