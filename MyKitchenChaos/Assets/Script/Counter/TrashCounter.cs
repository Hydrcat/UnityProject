public class TrashCounter :BaseCounter
{
    // 实现效果：交互时，玩家手上如果有垃圾，则删除。

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();
            // 播放动画
        }
    }

}
