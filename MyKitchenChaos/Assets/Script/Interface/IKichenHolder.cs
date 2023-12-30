using UnityEngine;

public interface IKichenHolder
{
    // 厨房道具挂点接口
    public KitchenObject GetKitchenObject();

    public void SetKitchenObject(KitchenObject kObject);

    public GameObject GetKitchenObjectHolder();

    public bool HasKitchenObject();

    public void ClearKitchenObject();
}