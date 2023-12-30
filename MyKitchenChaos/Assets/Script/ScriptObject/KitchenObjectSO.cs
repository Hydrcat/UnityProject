using UnityEngine;

[CreateAssetMenu()] // 使用这个特性可以将scriptObject加入到右键资产创建快捷菜单中
public class KitchenObjectSO : ScriptableObject
{
    public KitchenObject prefab;
    public Sprite sprite;
    public string objectName;
    public KitchenObject SliceObject;
    public int TargetCuttinCountNum;

    // 是否可以进行进行烹饪，如果可以，读取烹饪列表
    public bool isStovable = false;
    public float StovingTime ;
    public KitchenObject StovingObject ;
    
}
