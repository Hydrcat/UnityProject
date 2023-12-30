public class ClearCounter : BaseCounter
{
    // 普通柜台的交互 - 放下道具 或者 取走已经存在的道具
    public override void Interact(Player player)
    {
        // 放下道具
        if (player.GetKitchenObject() != null && this.GetKitchenObject() == null) 
        {
            KitchenObject kitchenObject = player.GetKitchenObject();
            kitchenObject.SetKitchenObjectHolder(this);
            return;
        }

        // 取走道具
        if (player.GetKitchenObject() == null && this.GetKitchenObject() != null)
        {
            KitchenObject kitchenObject = this.GetKitchenObject();
            kitchenObject.SetKitchenObjectHolder(player);
            return;
        }

    }
}
